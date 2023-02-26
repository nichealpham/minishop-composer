const listRouteView = [
  {
    to: { name: "home" },
    title: "Home",
    icon: "mdi-home-outline",
    tag: "button",
    devices: ["web", "mobile"],
    roles: ["patient", "staff"],
  },
  {
    to: { name: "patient" },
    title: "Patient",
    icon: "mdi-shield-account-outline",
    tag: "button",
    devices: ["web", "mobile"],
    roles: ["staff"],
  },
  {
    to: { name: "booking" },
    title: "Booking",
    icon: "mdi-calendar-month-outline",
    tag: "button",
    devices: ["web", "mobile"],
    roles: ["patient", "staff"],
  },
  {
    to: { name: "episode" },
    title: "Episode",
    icon: "mdi-clipboard-file-outline",
    tag: "button",
    devices: ["web", "mobile"],
    roles: ["patient", "staff"],
  },
  {
    to: { name: "search" },
    title: "Search",
    icon: "mdi-magnify",
    tag: "button",
    devices: [],
    roles: ["patient", "staff"],
  },
  {
    to: { name: "setting" },
    title: "Setting",
    icon: "mdi-hammer-wrench",
    tag: "button",
    devices: ["web"],
    roles: ["patient", "staff"],
  },
];

export default listRouteView;
