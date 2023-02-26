const routes = [
  {
    path: "/personal",
    component: () => import("@/components/PrimaryLayout"),
    children: [
      {
        path: "account",
        name: "account",
        meta: {
          title: "Personal account",
        },
        component: () => import("@/views/home/Home"),
      },
    ],
  },
];

export default routes;
