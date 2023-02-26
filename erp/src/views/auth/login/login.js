import { OAuthSignIn, OAuthProviderType } from "@/plugins/firebase";
import store from "@/stores";
import router from "@/router";

export async function login(httpClient, email, password) {
  var body = {
    email,
    password,
  };
  return await httpClient.post("/user/login", {}, body);
}

export async function oauth2Login(
  httpClient,
  provider = OAuthProviderType.Google
) {
  var token = await OAuthSignIn(provider);
  var result = await httpClient.post("/user/login/oauth2", null, null, {
    token,
  });
  if (result.token) return result;
  store.commit("Signup/SET_OAUTH_TOKEN", token);
  router.push("/register");
}
