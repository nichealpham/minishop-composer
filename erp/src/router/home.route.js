const routes = [
  {
    path: "/",
    component: () => import("@/components/PrimaryLayout"),
    redirect: { name: "home" },
    meta: {
      requireAuth: true,
    },
    children: [
      {
        path: "home",
        name: "home",
        meta: {
          title: "Home Page",
        },
        component: () => import("@/views/home/Home"),
      },
      {
        path: "booking",
        name: "booking",
        meta: {
          title: "Appointment page",
        },
        component: () => import("@/views/booking/Booking"),
      },
      {
        path: "episode",
        name: "episode",
        meta: {
          title: "Episode page",
        },
        component: () => import("@/views/episode/Episode"),
      },
      {
        path: "patient",
        name: "patient",
        meta: {
          title: "Patient page",
        },
        component: () => import("@/views/patient/Patient"),
      },
      {
        path: "setting",
        name: "setting",
        meta: {
          title: "Setting page",
        },
        component: () => import("@/views/setting/Setting"),
      },
      {
        path: "search",
        name: "search",
        meta: {
          title: "Search page",
        },
        component: () => import("@/views/search/Search"),
      },
    ],
  },
];

export default routes;
