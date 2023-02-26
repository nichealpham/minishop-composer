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
using Microsoft.Extensions.Configuration;

namespace Api.Business
{
    public static class PublicHelper
    {
        public static IQueryable<Episode> GetPublicEpisodeQueryable(this ERPContext _Context)
        {
            return _Context.Episodes
                .Include(a => a.Records)
                    .ThenInclude(r => r.Service);
        }

        public static SearchResult<EpisodeModelPublic> GetPublicEpisodeResults(this IQueryable<Episode> query, string keySearch = "", int page = 1, int limit = 10)
        {
            query = query.Where(a => a.PublicStatus == true);

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(a =>
                    (a.PublicHeadline != null && ApplicationDBContext.Standardizing(a.PublicHeadline).Contains(keySearch)) ||
                    (a.PublicTitle != null && ApplicationDBContext.Standardizing(a.PublicTitle).Contains(keySearch)));
            }

            var totals = query.Count();
            var items = query
                .OrderByDescending(p => p.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(EpisodeModelPublic.Convert)
                .ToList();

            return new SearchResult<EpisodeModelPublic>()
            {
                Totals = totals,
                Items = items
            };
        }
    }

    public class PublicBusiness
    {
        private readonly ERPContext _Context;
        private readonly EpisodeBusiness _EpisodeBusiness;
        private readonly ActivityBusiness _ActivityBusiness;
        private readonly string SandrasoftUri;

        public PublicBusiness(ERPContext context, EpisodeBusiness EpisodeBusiness, ActivityBusiness ActivityBusiness, IConfiguration Configuration)
        {
            _Context = context;
            _EpisodeBusiness = EpisodeBusiness;
            _ActivityBusiness = ActivityBusiness;
            SandrasoftUri = Configuration["SandrasoftUri"];
        }

        public bool CreateSaleOrder(string userCreateID, string userCreateEmail, PublicOrderCreate order)
        {
            var uid = Guid.Parse(userCreateID);
            var user = _Context.Users.AsNoTracking().FirstOrDefault(u => u.UserID == uid);
            if (user == null)
            {
                return false;
            }

            var asids = order.OrderItems.Select(i => Guid.Parse(i.ProductID)).ToList();
            var assets = _Context.Assets.Where(a => asids.Contains(a.AssetID)).ToList();

            var clids = assets.Select(a => a.ClinicID).Distinct().ToList();
            var services = _Context.Services.Include(s => s.Providers).Where(s =>
                    s.ClinicID.HasValue &&
                    clids.Contains(s.ClinicID.Value) &&
                    s.IsSaleOrder == true)
                .ToList();

            // Create each episode for each clinicID
            foreach (var clid in clids)
            {
                // check if user has been registered inside this clinic!
                if (!_Context.Roles.Any(role => role.UserID == uid && role.ClinicID == clid))
                {
                    _Context.Roles.Add(new Role()
                    {
                        ClinicID = clid,
                        UserID = uid,
                        RoleType = (byte)RoleType.Patient,
                    });
                    _Context.SaveChanges();
                }

                // Find the sale order service
                var service = services.Where(s => s.ClinicID == clid).FirstOrDefault();
                // if not found any sale order service => create a default service for ordering
                if (service == null)
                {
                    service = new Service()
                    {
                        ServiceName = "Đơn bán hàng",
                        ClinicID = clid,
                        Price = 0,
                        ShortDescription = "Đơn bán hàng",
                        IsSaleOrder = true,
                    };
                    var owner = _Context.Roles.FirstOrDefault(role => role.ClinicID == clid && role.RoleType == (int)RoleType.Owner);
                    if (owner == null) continue;

                    service.Providers.Add(new Provider()
                    {
                        ClinicID = clid,
                        UserID = owner.UserID,
                    });
                    _Context.Services.Add(service);
                    _Context.SaveChanges();
                }

                var description = String.Format(@"
                    <p>Họ tên: <b>{0}</b></p>
                    <p>Điện thoại: <b>{1}</b></p>
                    <p>Giới tính: <b>{2}</b></p>
                    <p>Địa chỉ giao hàng: <b>{3}</b></p>
                    <p>Ghi chú đơn hàng: {4}</p>
                    <p><b>Biểu mẫu bán hàng:</b></p>
                    {5}
                ", user.FullName, user.Phone, user.GenderType == true ? "Male" : "Female", order.ShippingAddress, order.OrderNote, service.ShortDescription);

                var stringAssetsTable = "";
                // Add assets into this episode
                for (var i = 0; i < order.OrderItems.Count; i++)
                {
                    var consume = order.OrderItems[i];
                    // check if amount remains is qualified
                    var asset = assets.FirstOrDefault(asset =>
                        asset.ClinicID == clid &&
                        asset.AssetID == Guid.Parse(consume.ProductID));

                    if (asset == null) continue;
                    stringAssetsTable += String.Format(@"
                        <tr>
                            <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'>{0}</span></td>
                            <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'><a href='{1}' class='ck-link_selected'>{2}</a></span></td>
                            <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'>{3}</span></td>
                            <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'>{4}</span></td>
                            <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'>{5}</span></td>
                            <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'>{6}</span></td>
                        </tr>",
                        i + 1,
                        string.Format(@"{0}/home?assetid={1}", SandrasoftUri, asset.AssetID),
                        asset.AssetName,
                        consume.Amount,
                        asset.Price?.ToString("N0"),
                        "<br data-cke-filler='true'>",
                        "<br data-cke-filler='true'>");
                }

                if (!string.IsNullOrEmpty(stringAssetsTable))
                {
                    stringAssetsTable = String.Format(@"
                    <figure class='table ck-widget ck-widget_with-selection-handle' contenteditable='false'>
                        <div class='ck ck-widget__selection-handle'></div>
                        <table>
                            <tbody>
                                <tr>
                                    <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'><b>STT</b></span></td>
                                    <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'><b>Tên sản phẩm</b></span></td>
                                    <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'><b>Số lượng</b></span></td>
                                    <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'><b>Đơn giá</b></span></td>
                                    <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'><b>Tình trạng</b></span></td>
                                    <td class='ck-editor__editable ck-editor__nested-editable' contenteditable='true'><span class='ck-table-bogus-paragraph'><b>Ghi chú</b></span></td>
                                </tr>
                                {0}
                            </tbody>
                        </table>
                    </figure>", stringAssetsTable);
                }

                description += stringAssetsTable;

                // if yes, then...
                // Check for user handling the sale order request
                var userAppointID = service.Providers.FirstOrDefault()?.UserID;
                if (userAppointID == null)
                {
                    continue;
                };

                var titleOrder = String.Format(@"{0}: {1}", service.ServiceName, String.Join(", ", assets.Where(a => a.ClinicID == clid).Select(s => s.AssetName).ToList()));
                // create episode and episode-record
                var episode = new Episode()
                {
                    ClinicID = clid,
                    UserAdmittedID = uid,
                    AdmissionType = (int)AdmissionType.CheckedIn,
                    TimeStart = DateTime.Now.ToUniversalTime(),
                    StatusID = (byte)EpisodeStatusType.CheckedIn,
                    PublicTitle = titleOrder,
                };
                episode.Records.Add(new Record()
                {
                    UserAppointID = userAppointID.Value,
                    ServiceID = service.ServiceID,
                    Price = service.Price,
                    ClinicalResult = description,
                });
                _Context.Episodes.Add(episode);
                _Context.SaveChanges();

                // Sendmail confirm episode for buyer
                if (!string.IsNullOrEmpty(userCreateEmail))
                {
                    _EpisodeBusiness.EpisodeSendMailAsync(new EpisodeSendMailContent()
                    {
                        dest = userCreateEmail,
                        subject = "Ghi nhận đơn hàng.",
                        content = String.Format(@"
                            <p><b>Xin chào <b>{0},</b> đơn hàng của bạn đã được ghi nhận.</b></p>
                            <p>{1}</p>
                            <p>Sau đây là thông tin liên lạc mà shop nhận được:</p>
                            <p>- Điện thoại: <b>{2}</b></p>
                            <p>- Địa chỉ giao hàng: <b>{3}</b></p>
                            <p>- Ghi chú đơn hàng: {4}</p>
                            <p>- <b>Ghi chú khác:</b>{5}</p>
                            <a style=""text-decoration: none;"" href=""{6}/episode?id={7}"">Xem chi tiết đơn hàng</a>
                            {8}
                            <p>Xin cảm ơn bạn.</p>
                        ", user.FullName, titleOrder, user.Phone, order.ShippingAddress, order.OrderNote, service.ShortDescription, SandrasoftUri, episode.EpisodeID, stringAssetsTable)
                    });
                }

                // Sendmail confirm episode for admin shop
                var providerIDs = service.Providers.Select(p => p.UserID).ToList();
                var userAppoints = _Context.Users.AsNoTracking().Where(s => providerIDs.Contains(s.UserID)).ToList();

                foreach (var userAppoint in userAppoints) {
                    var userAppointEmail = userAppoint.Email;
                    var userAppointName = userAppoint.FullName;
                    if (!string.IsNullOrEmpty(userAppointEmail))
                    {
                        _EpisodeBusiness.EpisodeSendMailAsync(new EpisodeSendMailContent()
                        {
                            dest = userAppointEmail,
                            subject = "Đơn hàng mới.",
                            content = String.Format(@"
                                <p><b>Xin chào <b>{0}</b>, bạn có đơn hàng mới!</b></p>
                                <p>{1}</p>
                                <p>Thông tin liên lạc của người mua hàng:</p>
                                <p>- Điện thoại: <b>{2}</b></p>
                                <p>- Địa chỉ giao hàng: <b>{3}</b></p>
                                <p>- Ghi chú đơn hàng: {4}</p>
                                <p>- <b>Ghi chú khác:</b>{5}</p>
                                <a style=""text-decoration: none;"" href=""{6}/episode?id={7}"">Chi tiết đơn hàng</a>
                                {8}
                                <p>Cảm ơn bạn đã chọn Sandrasoft.</p>
                            ", userAppointName, titleOrder, user.Phone, order.ShippingAddress, order.OrderNote, service.ShortDescription, SandrasoftUri, episode.EpisodeID, stringAssetsTable)
                        });
                    }
                }

                var metadata = _Context.GetEpisodeQueryable()
                    .Where(e => e.EpisodeID == episode.EpisodeID)
                    .Select(EpisodeDetail.Convert)
                    .FirstOrDefault();

                // Create notification and activity
                _ActivityBusiness.Create(userAppointID.Value.ToString(), new ActivityCreate()
                {
                    ServiceCode = (int)ServiceCode.Episode,
                    ActionCode = (int)ActionCode.Checkin,
                    StatusCode = (int)StatusCode.Info,
                    ClinicID = episode.ClinicID,
                    TargetItemID = episode.EpisodeID,
                    Metadata = metadata.ToJsonString(),
                });

                // Add asset into this episode, episode record
                foreach (var consume in order.OrderItems)
                {
                    // check if amount remains is qualified
                    var asset = assets.FirstOrDefault(asset =>
                        asset.ClinicID == clid &&
                        asset.AssetID == Guid.Parse(consume.ProductID));

                    if (asset == null) continue;
                    // Update amount in stocks if asset type == consumable
                    if (asset.Type == (byte)AssetType.Consumable)
                    {
                        asset.Amount -= consume.Amount;
                    }
                    // Create asset consumes
                    _Context.AssetConsumeds.Add(new AssetConsumed()
                    {
                        AssetID = asset.AssetID,
                        Price = asset.Price,
                        Amount = consume.Amount,
                        Description = String.Format(@"Sale order {0}", episode.EpisodeID),
                        ServiceID = service.ServiceID,
                        EpisodeID = episode.EpisodeID,
                        UserCreatedID = uid,
                    });
                }    
                _Context.SaveChanges();
            }
            return true;
        }

        public SearchResult<AttachmentAssetForSale> SearchAttachmentAssetsForSale(string companyName, string keySearch, int page = 1, int limit = 10)
        {
            var cid = Guid.Parse(GetCompanyID(companyName));

            var query = _Context.Assets.AsNoTracking()
                .Where(a => a.ClinicID == cid)
                .Where(a => a.IsSale == true);

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(a => ApplicationDBContext.Standardizing(a.AssetName).Contains(keySearch));
            }

            var assetIDs = query.Select(a => a.AssetID).ToList();

            var query2 = _Context.Attachments.AsNoTracking()
                .Where(at => at.TargetItemType == (int)CategoryType.Asset)
                .Where(at => assetIDs.Contains(at.TargetItemID)).ToList();

            var result = query2.Select(AttachmentAssetForSale.Convert).Skip((page - 1) * limit).Take(limit).ToList();

            return new SearchResult<AttachmentAssetForSale>()
            {
                Items = result,
            };
        }

        #region Company Functions
        public PublicCompanyModel GetCompany(string companyName)
        {
            companyName = companyName.Standardizing().Replace(" ", "");

            return _Context.Clinics.AsEnumerable()
                .Where(c => c.ClinicName.Standardizing().Replace(" ", "") == companyName)
                .Select(PublicCompanyModel.Convert)
                .FirstOrDefault();
        }

        public string GetCompanyID(string companyName)
        {
            companyName = companyName.Standardizing().Replace(" ", "");

            return _Context.Clinics.AsEnumerable()
                .Where(c => c.ClinicName.Standardizing().Replace(" ", "") == companyName)
                .Select(c => c.ClinicID.ToString())
                .FirstOrDefault();
        }
        #endregion

        #region Public Blog APIs
        public SearchResult<PublicBlogModel> SearchPublicBlogs(string companyName, List<string> categoryIDs = null, string keySearch = "", int page = 1, int limit = 10)
        {
            var cid = Guid.Parse(GetCompanyID(companyName));

            var query = _Context.Records.AsNoTracking()
                .Include(r => r.Episode)
                .Include(r => r.UserAppoint)
                .Where(r => r.Episode.ClinicID == cid)
                .Where(r => r.Episode.PublicStatus == true);

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(a => a.Episode.PublicTitle != null && ApplicationDBContext.Standardizing(a.Episode.PublicTitle).Contains(keySearch));
            }

            if (categoryIDs != null)
            {
                var categoryGuids = categoryIDs.Select(id => Guid.Parse(id));
                var epGuids = _Context.CategoryItems.AsNoTracking()
                    .Where(i => categoryGuids.Contains(i.CategoryID))
                    .Where(i => i.ItemType == (byte)CategoryType.Episode)
                    .Select(i => i.ItemID)
                    .ToList();

                query = query.Where(a => epGuids.Contains(a.EpisodeID));
            }

            var totals = query.Count();
            var items = query
                .OrderByDescending(p => p.DateUpdated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(PublicBlogModel.Convert)
                .ToList();

            var eids = items.Select(i => i.BlogID).ToList();

            // Map Images
            var images = _Context.Attachments.AsNoTracking()
                .Where(a => eids.Contains(a.TargetItemID))
                .Where(a => a.AttachmentType == (byte)AttachmentType.Image)
                .Select(a => new { a.AttachmentID, a.AttachmentUrl, a.TargetItemID })
                .ToList();

            items.ForEach(item =>
            {
                item.Images = images.Where(i => i.TargetItemID == item.BlogID).Select(i => new PublicImageModel()
                {
                    ImageUrl = i.AttachmentUrl,
                    AttachmentID = i.AttachmentID,
                }).ToList();
            });

            // Map Categories
            var categories = _Context.CategoryItems.Include(i => i.Category).AsNoTracking()
                .Where(i => eids.Contains(i.ItemID))
                .Where(i => i.ItemType == (byte)CategoryType.Episode)
                .Select(i => new { i.CategoryID, i.Category.CategoryName, i.ItemID })
                .ToList();

            items.ForEach(item =>
            {
                item.Categories = categories.Where(i => i.ItemID == item.BlogID).Select(i => new PublicCategoryModel()
                {
                    CategoryName = i.CategoryName,
                    CategoryID = i.CategoryID,
                }).ToList();
            });

            return new SearchResult<PublicBlogModel>()
            {
                Totals = totals,
                Items = items,
            };
        }

        public PublicBlogModel GetDetailBlog(string blogID)
        {
            var bid = Guid.Parse(blogID);

            var blog = _Context.Records.AsNoTracking()
                .Include(r => r.Episode)
                .Include(r => r.UserAppoint)
                .Where(r => r.Episode.EpisodeID == bid)
                .Where(r => r.Episode.PublicStatus == true)
                .Select(PublicBlogModel.ConvertDetail)
                .FirstOrDefault();

            // Map Images
            blog.Images = _Context.Attachments.AsNoTracking()
                .Where(a => a.TargetItemID == bid)
                .Where(a => a.AttachmentType == (byte)AttachmentType.Image)
                .Select(a => new PublicImageModel()
                {
                    ImageUrl = a.AttachmentUrl,
                    AttachmentID = a.AttachmentID,
                })
                .ToList();


            // Map Categories
            blog.Categories = _Context.CategoryItems.Include(i => i.Category).AsNoTracking()
                .Where(i => i.ItemID == bid)
                .Where(i => i.ItemType == (byte)CategoryType.Episode)
                .Select(i => new PublicCategoryModel()
                {
                    CategoryName = i.Category.CategoryName,
                    CategoryID = i.CategoryID,
                })
                .ToList();

            var episode = _Context.Episodes.FirstOrDefault(e => e.EpisodeID == bid);
            if (episode.ViewCount == null) episode.ViewCount = 0;
            episode.ViewCount += 1;
            _Context.SaveChanges();

            return blog;
        }

        public string GetDetailDescriptionBlog(string blogID)
        {
            var bid = Guid.Parse(blogID);
            var episode = _Context.Episodes.FirstOrDefault(e => e.EpisodeID == bid);
            if (episode.ViewCount == null) episode.ViewCount = 0;
            episode.ViewCount += 1;
            _Context.SaveChanges();
            return _Context.Records.AsNoTracking().Where(a => a.EpisodeID == bid).Select(a => a.ClinicalResult).FirstOrDefault();
        }
        #endregion

        #region Public Product APIs
        public SearchResult<PublicProductModel> SearchPublicProducts(string companyName, List<string> categoryIDs = null, int? productType = null, decimal? priceLower = null, decimal? priceUpper = null, string keySearch = "", int page = 1, int limit = 10)
        {
            var companyID = GetCompanyID(companyName);
            var cid = Guid.Parse(companyID);

            var query = _Context.Assets.AsNoTracking()
                .Where(a => a.StatusID == (int)StatusType.Active)
                .Where(a => a.ClinicID == cid)
                .Where(a => a.IsSale == true);

            if (productType.HasValue)
            {
                query = query.Where(a => a.Type == productType.Value);
            }

            if (priceLower.HasValue)
            {
                query = query.Where(a => a.Price >= priceLower.Value);
            }

            if (priceUpper.HasValue)
            {
                query = query.Where(a => a.Price <= priceUpper.Value);
            }

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(a => ApplicationDBContext.Standardizing(a.AssetName).Contains(keySearch));
            }

            if (categoryIDs != null)
            {
                var categoryGuids = categoryIDs.Select(id => Guid.Parse(id));
                var productGuids = _Context.CategoryItems.AsNoTracking()
                    .Where(i => categoryGuids.Contains(i.CategoryID))
                    .Where(i => i.ItemType == (byte)CategoryType.Asset)
                    .Select(i => i.ItemID)
                    .ToList();

                query = query.Where(a => productGuids.Contains(a.AssetID));
            }

            var totals = query.Count();
            var items = query
                .OrderByDescending(p => p.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(PublicProductModel.Convert)
                .ToList();

            var pids = items.Select(i => i.ProductID).ToList();

            // Map Images
            var images = _Context.Attachments.AsNoTracking()
                .Where(a => pids.Contains(a.TargetItemID))
                .Where(a => a.AttachmentType == (byte)AttachmentType.Image)
                .Select(a => new { a.AttachmentID, a.AttachmentUrl, a.TargetItemID })
                .ToList();

            items.ForEach(item =>
            {
                item.Images = images.Where(i => i.TargetItemID == item.ProductID).Select(i => new PublicImageModel()
                {
                    ImageUrl = i.AttachmentUrl,
                    AttachmentID = i.AttachmentID,
                }).ToList();
            });

            // Map Categories
            var categories = _Context.CategoryItems.Include(i => i.Category).AsNoTracking()
                .Where(i => pids.Contains(i.ItemID))
                .Where(i => i.ItemType == (byte)CategoryType.Asset)
                .Select(i => new { i.CategoryID, i.Category.CategoryName, i.ItemID })
                .ToList();

            items.ForEach(item =>
            {
                item.Categories = categories.Where(i => i.ItemID == item.ProductID).Select(i => new PublicCategoryModel()
                {
                    CategoryName = i.CategoryName,
                    CategoryID = i.CategoryID,
                }).ToList();
            });


            return new SearchResult<PublicProductModel>()
            {
                Totals = totals,
                Items = items,
            };
        }

        public PublicProductModel GetDetailProduct(string productID)
        {
            var pid = Guid.Parse(productID);

            var product = _Context.Assets.AsNoTracking()
                .Where(a => a.AssetID == pid)
                .Where(a => a.IsSale == true)
                .Select(PublicProductModel.ConvertDetail)
                .FirstOrDefault();

            // Map Images
            product.Images = _Context.Attachments.AsNoTracking()
                .Where(a => a.TargetItemID == pid)
                .Where(a => a.AttachmentType == (byte)AttachmentType.Image)
                .Select(a => new PublicImageModel()
                {
                    ImageUrl = a.AttachmentUrl,
                    AttachmentID = a.AttachmentID,
                })
                .ToList();


            // Map Categories
            product.Categories = _Context.CategoryItems.Include(i => i.Category).AsNoTracking()
                .Where(i => i.ItemID == pid)
                .Where(i => i.ItemType == (byte)CategoryType.Asset)
                .Select(i => new PublicCategoryModel()
                {
                    CategoryName = i.Category.CategoryName,
                    CategoryID = i.CategoryID,
                })
                .ToList();

            return product;
        }

        public string GetDetailDescriptionProduct(string productID)
        {
            var pid = Guid.Parse(productID);
            return _Context.Assets.AsNoTracking().Where(a => a.AssetID == pid && a.IsSale == true).Select(a => a.Description).FirstOrDefault();
        }
        #endregion

        #region Legalcy business
        public EpisodeDetailPublic GetPublicEpisode(string publicNameUrl)
        {
            publicNameUrl = publicNameUrl.Standardizing();

            var episode = _Context.GetPublicEpisodeQueryable()
                .Where(e => ApplicationDBContext.Standardizing(e.PublicNameUrl) == publicNameUrl)
                .Where(e => e.PublicStatus == true)
                .FirstOrDefault();

            if (episode == null)
            {
                return null;
            }

            if (episode.ViewCount == null) episode.ViewCount = 0;
            episode.ViewCount += 1;

            _Context.SaveChanges();

            return EpisodeDetailPublic.Convert(episode);
        }

        public SearchResult<EpisodeModelPublic> SearchPublicEpisodes(string clinicID, string keySearch, int page = 1, int limit = 10)
        {
            var cid = Guid.Parse(clinicID);

            return _Context
                .GetPublicEpisodeQueryable()
                .Where(e => e.ClinicID == cid)
                .GetPublicEpisodeResults(keySearch, page, limit);
        }
        #endregion
    }
}
