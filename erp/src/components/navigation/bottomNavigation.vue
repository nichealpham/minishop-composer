<template>
  <v-bottom-navigation class="bottom_navigation_drawer" fixed grow>
    <v-btn
      v-for="item in routes"
      :key="item.title"
      :to="item.to"
      :style="{ width: 100 / routes.length + 'vw !important' }"
      exact
    >
      <span class="text-capitalize">{{
        $t(`nav.${item.title.toLowerCase()}`)
      }}</span>
      <v-icon>{{ item.icon }}</v-icon>
    </v-btn>
  </v-bottom-navigation>
</template>

<script>
import ListRouteView from "./routesView";
export default {
  computed: {
    routes() {
      var items = ListRouteView.filter((i) => i.devices.includes("mobile"));
      var isStaff = this.$store.getters["Authen/isClinicStaff"];
      if (isStaff) {
        return items.filter((i) => i.roles.includes("staff"));
      }
      return items.filter((i) => i.roles.includes("patient"));
    },
  },
  data: function() {
    return {};
  },
};
</script>

<style lang="scss">
.bottom_navigation_drawer {
  .v-btn .v-btn__content {
    font-size: 0.8rem;
  }
}
</style>
