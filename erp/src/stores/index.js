import Vue from "vue";
import Vuex from "vuex";

import Authen from "./authen.store";
import Register from "./register.store";
import Theme from "./theme.store";
import Signup from "./signup.store";
import Mobile from "./mobile.store";
import Asset from "./asset.store";
import Public from "./public.store";
import Fcm from "./fcm.store";
import MiniApp from "./miniapp.store";

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    Authen,
    Register,
    Theme,
    Signup,
    Mobile,
    Asset,
    Public,
    Fcm,
    MiniApp,
  },
});
