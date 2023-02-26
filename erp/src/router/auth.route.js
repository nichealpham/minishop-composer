const routes = [
  {
    path: "/login",
    alias: "/authenticate",
    meta: {
      title: "Sandrasoft login",
      requireAuth: false,
    },
    component: () => import("@/layouts/background"),
    children: [
      {
        path: "/",
        name: "login",
        component: () => import("@/views/auth/login/Login.vue"),
      },
    ],
  },
  {
    path: "/register",
    meta: {
      title: "Register account",
      requireAuth: false,
    },
    component: () => import("@/layouts/background"),
    children: [
      {
        path: "/",
        name: "register",
        component: () => import("@/views/auth/signUp/register"),
      },
    ],
  },
  {
    path: "/signup",
    meta: {
      title: "Sign up account",
      requireAuth: false,
    },
    component: () => import("@/layouts/background"),
    children: [
      {
        path: "/",
        name: "signup",
        component: () => import("@/views/auth/signUp/signup"),
      },
    ],
  },
  {
    path: "/clinic",
    meta: {
      title: "Sign up account",
      requireAuth: false,
    },
    component: () => import("@/layouts/background"),
    children: [
      {
        path: "/",
        name: "clinic",
        component: () => import("@/views/auth/signUp/signup"),
      },
    ],
  },
];

export default routes;
