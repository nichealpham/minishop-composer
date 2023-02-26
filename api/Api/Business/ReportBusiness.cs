using AppGlobal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppGlobal.Entities;
using AppGlobal.Commons;
using AppGlobal.Models;
using AppGlobal.DBContext;
using Microsoft.EntityFrameworkCore;
using AppGlobal.Extensions;
using FirebaseAdmin.Auth;
using AppGlobal.Constants;
using Api.Extensions;
using OfficeOpenXml;
using Microsoft.Extensions.Configuration;

namespace Api.Business
{
    public class ReportBusiness
    {
        private readonly ERPContext _Context;
        private readonly string SandrasoftUri;
        public ReportBusiness(ERPContext context, IConfiguration Configuration)
        {
            _Context = context;
            SandrasoftUri = Configuration["SandrasoftUri"];
        }

        #region Stats API
        public RevenueStatistics GetRevenueStats(string clinicID)
        {
            var cid = Guid.Parse(clinicID);
            var query = _Context.Invoices
                .Include(i => i.Episode)
                .AsNoTracking()
                .Where(i => i.Episode.ClinicID == cid);
            var now = DateTime.Now;

            var todayStart = now.StartOfDay().ToUniversalTime();
            var todayEnd = now.EndOfDay().ToUniversalTime();
            var monthStart = now.StartOfMonth().ToUniversalTime();
            var monthEnd = now.EndOfMonth().ToUniversalTime();
            var quarterStart = now.StartOfQuarter().ToUniversalTime();
            var quarterEnd = now.EndOfQuarter().ToUniversalTime();

            var revenues = query
                .Where(i => i.DateCreated >= quarterStart && i.DateCreated <= quarterEnd)
                .Select(i => new { i.DateCreated, i.FinalPrice })
                .ToList();

            var dayRevenue = revenues.Where(i => i.DateCreated >= todayStart && i.DateCreated <= todayEnd).Sum(i => i.FinalPrice);
            var monthRevenue = revenues.Where(i => i.DateCreated >= monthStart && i.DateCreated <= monthEnd).Sum(i => i.FinalPrice);
            var quarterRevenue = revenues.Where(i => i.DateCreated >= quarterStart && i.DateCreated <= quarterEnd).Sum(i => i.FinalPrice);

            return new RevenueStatistics()
            {
                Day = dayRevenue,
                Month = monthRevenue,
                Quater = quarterRevenue,
            };
        }

        public UserStatistics GetUserStats(string userID)
        {
            var guid = Guid.Parse(userID);
            var query = _Context.Records
                .Include(r => r.Episode)
                .AsNoTracking();

            var episodes = query
                .Where(e => e.Episode.UserAdmittedID == guid || e.UserAppointID == guid)
                .Select(e => new { e.UserAppointID, e.Episode.ClinicID, e.Episode.EpisodeID })
                .ToList();

            var countClinics = episodes.Select(e => e.ClinicID).Distinct().Count();
            var countDoctors = episodes.Select(e => e.UserAppointID).Distinct().Count();
            var countEpisodes = episodes.Select(e => e.EpisodeID).Distinct().Count();

            var countAppointments = _Context.Appointments
                .AsNoTracking()
                .Where(u => u.UserCreateID == guid || u.UserAppointID == guid)
                .Count();

            var countPatients = query
                .Where(e => e.UserAppointID == guid)
                .Select(e => e.Episode.UserAdmittedID)
                .Distinct()
                .Count();

            return new UserStatistics()
            {
                CountPatients = countPatients,
                CountAppointments = countAppointments,
                CountClinics = countClinics,
                CountDoctors = countDoctors,
                CountEpisodes = countEpisodes,
            };
        }
        #endregion

        #region Excel API
        public ExcelFileData GetRevenueExcel(string clinicID, DateTime? timeStart, DateTime? timeEnd)
        {
            var filename = string.Format(@"revenue-{0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fff"));

            var now = DateTime.Now;
            if (timeStart == null)
            {
                timeStart = now.StartOfMonth().ToUniversalTime();
            }
            else
            {
                timeStart = timeStart.Value.StartOfDay().ToUniversalTime();
            }
            if (timeEnd == null)
            {
                timeEnd = now.EndOfMonth().ToUniversalTime();
            }
            else
            {
                timeEnd = timeEnd.Value.EndOfDay().ToUniversalTime();
            }

            var services = GetRevenueServices(clinicID, timeStart.Value, timeEnd.Value);
            var assets = GetReveueAssets(clinicID, timeStart.Value, timeEnd.Value);
            var imports = GetImportAssets(clinicID, timeStart.Value, timeEnd.Value);
            var inventory = GetAssetInventory(clinicID);

            return new ExcelPackage()
                .EmbedRevenueServices(services)
                .EmbedRevenueAssets(assets)
                .EmbedInventoryAssets(inventory)
                .EmbedImportAssets(imports)
                .ToDataFile(filename);
        }

        public List<ServiceRevenue> GetRevenueServices(string clinicID, DateTime timeStart, DateTime timeEnd)
        {
            var cid = Guid.Parse(clinicID);
            timeStart = timeStart.StartOfDay().ToUniversalTime();
            timeEnd = timeEnd.EndOfDay().ToUniversalTime();

            var items = _Context.GetEpisodeQueryable()
                .Where(a => a.ClinicID == cid)
                .Where(a => a.StatusID == (byte)EpisodeStatusType.Success)
                .Where(a => a.DateCreated >= timeStart)
                .Where(a => a.DateCreated <= timeEnd)
                .ToList()
                .OrderBy(a => a.DateCreated)
                .Select(ServiceRevenue.Convert)
                .ToList();

            items.ForEach(s =>
            {
                s.EpisodeUri = string.Format(@"{0}{1}", SandrasoftUri, s.EpisodeUri);
                s.UserAppointUri = string.Format(@"{0}{1}", SandrasoftUri, s.UserAppointUri);
                s.UserAdmittedUri = string.Format(@"{0}{1}", SandrasoftUri, s.UserAdmittedUri);
            });

            return items;
        }

        public List<AssetInStock> GetAssetInventory(string clinicID)
        {
            var cid = Guid.Parse(clinicID);
            var items = _Context.Assets.AsNoTracking()
                .Where(a => a.ClinicID == cid)
                .Where(a => a.StatusID == (int)StatusType.Active)
                .ToList()
                .OrderBy(a => a.DateCreated)
                .Select(AssetInStock.Convert)
                .ToList();

            var userIDs = items.Select(i => i.UserCreatedID).ToList();
            var users = _Context.Users.AsNoTracking().Where(u => userIDs.Contains(u.UserID)).Select(u => new { u.UserID, u.FullName }).ToList();

            items.ForEach(s =>
            {
                s.AssetQrUri = string.Format(@"{0}{1}", SandrasoftUri, s.AssetQrUri);
                s.UserCreatedUri = string.Format(@"{0}{1}", SandrasoftUri, s.UserCreatedUri);
                s.UserCreatedName = users.FirstOrDefault(u => u.UserID == s.UserCreatedID).FullName;
            });

            return items;
        }

        public List<AssetRevenue> GetReveueAssets(string clinicID, DateTime timeStart, DateTime timeEnd)
        {
            var cid = Guid.Parse(clinicID);
            var items = _Context.AssetConsumeds.AsNoTracking()
                .Include(c => c.Asset)
                .Where(a => a.Asset.ClinicID == cid)
                .Where(a => a.DateCreated >= timeStart)
                .Where(a => a.DateCreated <= timeEnd)
                .OrderBy(a => a.DateCreated)
                .Select(AssetRevenue.Convert)
                .ToList();

            var userIDs = items.Select(i => i.UserCreatedID).ToList();
            var users = _Context.Users.AsNoTracking().Where(u => userIDs.Contains(u.UserID)).Select(u => new { u.UserID, u.FullName }).ToList();

            var serviceIDs = items.Select(i => i.ServiceID).ToList();
            var services = _Context.Services.AsNoTracking().Where(u => serviceIDs.Contains(u.ServiceID)).Select(u => new { u.ServiceID, u.ServiceName }).ToList();

            items.ForEach(s =>
            {
                s.Asset.AssetQrUri = string.Format(@"{0}{1}", SandrasoftUri, s.Asset.AssetQrUri);
                s.UserCreatedUri = string.Format(@"{0}{1}", SandrasoftUri, s.UserCreatedUri);
                s.EpisodeUri = string.Format(@"{0}{1}", SandrasoftUri, s.EpisodeUri);
                s.UserCreatedName = users.FirstOrDefault(u => u.UserID == s.UserCreatedID)?.FullName;
                s.ServiceName = services.FirstOrDefault(u => u.ServiceID == s.ServiceID).ServiceName;
            });

            return items;
        }

        public List<AssetImport> GetImportAssets(string clinicID, DateTime timeStart, DateTime timeEnd)
        {
            var cid = Guid.Parse(clinicID);
            var items = _Context.AssetImporteds.AsNoTracking()
                .Include(c => c.Asset)
                .Where(a => a.Asset.ClinicID == cid)
                .Where(a => a.DateCreated >= timeStart)
                .Where(a => a.DateCreated <= timeEnd)
                .OrderBy(a => a.DateCreated)
                .Select(AssetImport.Convert)
                .ToList();

            var userIDs = items.Select(i => i.UserCreatedID).ToList();
            var users = _Context.Users.AsNoTracking().Where(u => userIDs.Contains(u.UserID)).Select(u => new { u.UserID, u.FullName }).ToList();

            items.ForEach(s =>
            {
                s.Asset.AssetQrUri = string.Format(@"{0}{1}", SandrasoftUri, s.Asset.AssetQrUri);
                s.UserCreatedUri = string.Format(@"{0}{1}", SandrasoftUri, s.UserCreatedUri);
                s.UserCreatedName = users.FirstOrDefault(u => u.UserID == s.UserCreatedID).FullName;
            });

            return items;
        }
        #endregion
    }

    public static class ExcelPackageHelper
    {
        public static ExcelPackage EmbedRevenueServices(this ExcelPackage excelPackage, List<ServiceRevenue> services)
        {
            return excelPackage.AddWorksheet("Services Revenue", (ExcelWorksheet ws) =>
            {
                ws.WriteTitle("A1:L1", "SERVICES REVENUE");
                ws.WriteCellHeader("A2", "Episode");
                ws.WriteCellHeader("B2", "Time start");
                ws.WriteCellHeader("C2", "Time end");
                ws.WriteCellHeader("D2", "Minutes");
                ws.WriteCellHeader("E2:F2", "User");
                ws.WriteCellHeader("G2:H2", "Advisor");
                ws.WriteCellHeader("I2:K2", "Service");
                ws.WriteCellHeader("L2", "Price");

                var line = 2;
                for (var i = 0; i < services.Count; i++)
                {
                    line += 1;
                    var s = services[i];

                    ws.WriteCellFormula("A" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.EpisodeUri, "#" + (i + 1)));
                    ws.WriteCell("B" + line, s.TimeStart.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"));
                    ws.WriteCell("C" + line, s.TimeEnd.Value.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"));
                    ws.WriteCell("D" + line, Decimal.Round((decimal)(s.TimeEnd.Value.Subtract(s.TimeStart)).TotalMinutes));

                    ws.WriteCellFormula("E" + line + ":F" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.UserAdmittedUri, s.UserAdmittedName));
                    ws.WriteCellFormula("G" + line + ":H" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.UserAppointUri, s.UserAppointName));

                    ws.WriteCell("I" + line + ":K" + line, s.ServiceName);
                    ws.WriteCell("L" + line, s.TotalPrice);
                }
                if (services.Count > 0)
                {
                    line += 1;
                    ws.WriteCellHeader("A" + line + ":K" + line, "Total sum");
                    ws.WriteCellFormula("L" + line, string.Format(@"SUM(L3:L{0})", line - 1));
                }
            });
        }

        public static ExcelPackage EmbedInventoryAssets(this ExcelPackage excelPackage, List<AssetInStock> items)
        {
            return excelPackage.AddWorksheet("Inventory", (ExcelWorksheet ws) =>
            {
                ws.WriteTitle("A1:L1", "INVENTORY");
                ws.WriteCellHeader("A2", "Asset");
                ws.WriteCellHeader("B2:D2", "Name");
                ws.WriteCellHeader("E2", "Code");
                ws.WriteCellHeader("F2", "Type");
                ws.WriteCellHeader("G2:H2", "Created by");
                ws.WriteCellHeader("I2", "Last update");
                ws.WriteCellHeader("J2", "Price");
                ws.WriteCellHeader("K2", "Amount");
                ws.WriteCellHeader("L2", "Total");

                var line = 2;
                for (var i = 0; i < items.Count; i++)
                {
                    line += 1;
                    var s = items[i];

                    ws.WriteCellFormula("A" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.AssetQrUri, "#" + (i + 1)));
                    ws.WriteCellFormula("B" + line + ":D" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.AssetQrUri, s.AssetName));
                    ws.WriteCell("E" + line, string.IsNullOrEmpty(s.AssetCode) ? "NA" : s.AssetCode);
                    ws.WriteCell("F" + line, s.TypeName);
                    ws.WriteCellFormula("G" + line + ":H" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.UserCreatedUri, s.UserCreatedName));
                    ws.WriteCell("I" + line, s.DateUpdated.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"));
                    ws.WriteCell("J" + line, s.Price);
                    ws.WriteCell("K" + line, s.Amount);
                    ws.WriteCellFormula("L" + line, string.Format(@"J{0}*K{1}", line, line));
                }
                if (items.Count > 0)
                {
                    line += 1;
                    ws.WriteCellHeader("A" + line + ":K" + line, "Total sum");
                    ws.WriteCellFormula("L" + line, string.Format(@"SUM(L3:L{0})", line - 1));
                }
            });
        }

        public static ExcelPackage EmbedRevenueAssets(this ExcelPackage excelPackage, List<AssetRevenue> items)
        {
            return excelPackage.AddWorksheet("Assets Revenue", (ExcelWorksheet ws) =>
            {
                ws.WriteTitle("A1:S1", "ASSETS REVENUE");
                ws.WriteCellHeader("A2", "Episode");
                ws.WriteCellHeader("B2:C2", "Service");
                ws.WriteCellHeader("D2", "Code");
                ws.WriteCellHeader("E2", "Type");
                ws.WriteCellHeader("F2:I2", "Asset Name");
                ws.WriteCellHeader("J2:M2", "Description");
                ws.WriteCellHeader("N2:O2", "Created by");
                ws.WriteCellHeader("P2", "Date time");
                ws.WriteCellHeader("Q2", "Price");
                ws.WriteCellHeader("R2", "Amount");
                ws.WriteCellHeader("S2", "Total");

                var groups = items.GroupBy(i => i.EpisodeID, i => i, (key, items) => new
                {
                    EpisodeID = key,
                    Items = items
                }).ToList();

                var line = 2;

                for (var j = 0; j < groups.Count; j++)
                {
                    var group = groups[j];

                    var serviceName = group.Items.ToList()[0].ServiceName;
                    var episodeUri = group.Items.ToList()[0].EpisodeUri;
                    ws.WriteCellFormula("A" + (line + 1) + ":A" + (line + group.Items.ToList().Count), string.Format(@"HYPERLINK(""{0}"", ""{1}"")", episodeUri, "#" + (j + 1)));
                    ws.WriteCellFormula("B" + (line + 1) + ":C" + (line + group.Items.ToList().Count), string.Format(@"HYPERLINK(""{0}"", ""{1}"")", episodeUri, serviceName));

                    for (var i = 0; i < group.Items.ToList().Count; i++)
                    {
                        var s = group.Items.ToList()[i];

                        line += 1;
                        ws.WriteCell("D" + line, string.IsNullOrEmpty(s.Asset.AssetCode) ? "NA" : s.Asset.AssetCode);
                        ws.WriteCell("E" + line, s.Asset.TypeName);
                        ws.WriteCellFormula("F" + line + ":I" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.Asset.AssetQrUri, s.Asset.AssetName));
                        ws.WriteCell("J" + line + ":M" + line, string.IsNullOrEmpty(s.Description) ? "NA" : s.Description);
                        ws.WriteCellFormula("N" + line + ":O" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.UserCreatedUri, s.UserCreatedName));
                        ws.WriteCell("P" + line, s.DateCreated.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"));
                        ws.WriteCell("Q" + line, s.Price);
                        ws.WriteCell("R" + line, s.Amount);
                        ws.WriteCellFormula("S" + line, string.Format(@"Q{0}*R{1}", line, line));
                    }
                }

                if (items.Count > 0)
                {
                    line += 1;
                    ws.WriteCellHeader("A" + line + ":R" + line, "Total sum");
                    ws.WriteCellFormula("S" + line, string.Format(@"SUM(S3:S{0})", line - 1));
                }
            });
        }

        public static ExcelPackage EmbedImportAssets(this ExcelPackage excelPackage, List<AssetImport> items)
        {
            return excelPackage.AddWorksheet("Assets Imported", (ExcelWorksheet ws) =>
            {
                ws.WriteTitle("A1:P1", "ASSET IMPORTED");
                ws.WriteCellHeader("A2", "Asset");
                ws.WriteCellHeader("B2:D2", "Name");
                ws.WriteCellHeader("E2", "Code");
                ws.WriteCellHeader("F2", "Type");
                ws.WriteCellHeader("G2:H2", "Created by");
                ws.WriteCellHeader("I2", "Last update");
                ws.WriteCellHeader("J2:M2", "Description");
                ws.WriteCellHeader("N2", "Price");
                ws.WriteCellHeader("O2", "Amount");
                ws.WriteCellHeader("P2", "Total");

                var line = 2;
                for (var i = 0; i < items.Count; i++)
                {
                    line += 1;
                    var s = items[i];

                    ws.WriteCellFormula("A" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.Asset.AssetQrUri, "#" + (i + 1)));
                    ws.WriteCellFormula("B" + line + ":D" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.Asset.AssetQrUri, s.Asset.AssetName));
                    ws.WriteCell("E" + line, string.IsNullOrEmpty(s.Asset.AssetCode) ? "NA" : s.Asset.AssetCode);
                    ws.WriteCell("F" + line, s.Asset.TypeName);
                    ws.WriteCellFormula("G" + line + ":H" + line, string.Format(@"HYPERLINK(""{0}"", ""{1}"")", s.UserCreatedUri, s.UserCreatedName));
                    ws.WriteCell("I" + line, s.DateUpdated.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"));
                    ws.WriteCell("J" + line + ":M" + line, string.IsNullOrEmpty(s.Description) ? "N/A" : s.Description);
                    ws.WriteCell("N" + line, s.Price);
                    ws.WriteCell("O" + line, s.Amount);
                    ws.WriteCellFormula("P" + line, string.Format(@"N{0}*O{1}", line, line));
                }
                if (items.Count > 0)
                {
                    line += 1;
                    ws.WriteCellHeader("A" + line + ":O" + line, "Total sum");
                    ws.WriteCellFormula("P" + line, string.Format(@"SUM(P3:P{0})", line - 1));
                }
            });
        }
    }
}
