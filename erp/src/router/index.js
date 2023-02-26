import Vue from "vue";
import AuthRoutes from "./auth.route.js";
import HomeRoutes from "./home.route.js";
import PersonalRoutes from "./personal.route";
import VueRouter from "vue-router";
import { isAuthenticated } from "@/plugins/helpers";

Vue.use(VueRouter);

const routes = [
  {
    path: "*",
    redirect: { name: "booking" },
  },
  {
    path: "/logout",
    component: () => import("@/views/Logout"),
  },
  ...HomeRoutes,
  ...AuthRoutes,
  ...PersonalRoutes,
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

// beforeEach for each route when run
router.beforeEach((to, from, next) => {
  // check meta data that requireAuth or not
  const requireAuth = to.matched.some(
    (childRoute) => childRoute.meta.requireAuth
  );
  // check Authenticate if having
  if (requireAuth && !isAuthenticated()) {
    next({ name: "login" });
  } else {
    if (to.name == "login" && isAuthenticated()) {
      next({ name: "home" });
    }
    if (to.name == "register" && isAuthenticated()) {
      next({ name: "home" });
    }
    next();
  }
});

// afterEach to do something when navigate success
router.afterEach((to) => {
  document.title = to.meta.title ?? "App";
});
export default router;
