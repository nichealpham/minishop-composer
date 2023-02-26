<template>
  <div class="block__content report">
    <div class="title">
      <span class="text-h6 font-weight-bold d-block">
        {{ $t("home.report") }}
      </span>
    </div>
    <div style="text-align: center" v-if="loading" class="mt-2">
      <v-progress-circular indeterminate color="#fefefe"></v-progress-circular>
    </div>
    <!-- For small screen <= 1200px -->
    <div class="report__small__screen" v-else>
      <!-- Loop - only for testing - you can replace if you dont want to use array -->
      <template v-for="val in 4">
        <div
          class="report__item"
          :key="listReport[val - 1].title"
          v-ripple
          @click="$router.push(listReport[val - 1].redirect)"
        >
          <span class="box__icon">
            <span class="icon" v-bind:class="listReport[val - 1].class">
              <v-icon :size="30">{{ listReport[val - 1].icon }}</v-icon>
            </span>
          </span>
          <span class="caption" v-html="listReport[val - 1].title"></span>
          <span class="number text-h6">{{ listReport[val - 1].value }}</span>
        </div>
      </template>
    </div>
  </div>
</template>

<script>
export default {
  name: "CommonAmountReport",
  data: function () {
    return {
      loading: false,
      listReport: [
        {
          title: this.$t(`nav.patient`),
          class: "amount__patient",
          icon: "mdi-account-outline",
          value: 0,
          redirect: "/patient",
        },
        {
          title: this.$t(`nav.doctor`),
          class: "amount__doctor",
          icon: "mdi-doctor",
          value: 0,
          redirect: "/setting",
        },
        {
          title: this.$t(`nav.episode`),
          class: "amount__episode",
          icon: "mdi-clipboard-file-outline",
          value: 0,
          redirect: "/episode",
        },
        {
          title: this.$t(`nav.booking`),
          class: "amount__booking",
          icon: "mdi-calendar-range",
          value: 0,
          redirect: "/booking",
        },
        {
          title: this.$t(`nav.clinic`),
          class: "amount__clinic",
          icon: "mdi-hospital-marker",
          value: 0,
          redirect: "/setting",
        },
      ],
    };
  },
  mounted() {
    this.render();
  },
  methods: {
    async render() {
      this.loading = true;
      var result = await this.$httpClient.get(`/user/report/statistics`);
      this.loading = false;
      if (result.error) {
        return this.showError("Get user statistics errored!");
      }
      var {
        countPatients,
        countDoctors,
        countEpisodes,
        countAppointments,
        countClinics,
      } = result;
      this.listReport[0].value = countPatients || 0;
      this.listReport[1].value = countDoctors || 0;
      this.listReport[2].value = countEpisodes || 0;
      this.listReport[3].value = countAppointments || 0;
      this.listReport[4].value = countClinics || 0;
    },
  },
};
</script>

<style scoped lang="scss">
@import "style.scss";
</style>
