export async function registerClinic(httpClient, body) {
  return await httpClient.post("/owner/clinics", {}, body);
}
