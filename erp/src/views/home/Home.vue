<template>
  <page-content>
    <template v-slot:toolbar v-if="isClinicStaff">
      <div class="fit_toolbar_booking">
        <v-select
          class="ml-1"
          :items="reportOptions"
          v-model="monthSelected"
          outlined
          dense
          background-color="white"
        ></v-select>
        <v-btn
          color="#6166f5"
          class="white--text ml-3"
          @click="downloadReport"
          :loading="loading"
        >
          <v-icon left dark>
            mdi-download
          </v-icon>
          {{ $t("common.report") }}
        </v-btn>
      </div>
    </template>

    <template v-slot:main>
      <!-- Hey WELCOME -->
      <div v-ripple class="block__content welcome">
        <!-- Welcome box -->
        <div class="d-block">
          <h1 class="welcome__text">
            {{ $t("home.welcome") }}, <b>{{ userData.fullName }}</b>
          </h1>
          <span
            class="text-h5 take__nice__day"
            v-html="$t('home.haveaniceday')"
          >
          </span>
        </div>
        <!-- Spinner here -->
        <div class="spinner__area">
          <img :src="welcome.icon" />
        </div>
      </div>
      <div :class="{ no_toolbar: !isClinicStaff }">
        <!-- report number -->
        <common-amount-report />
        <!-- upcoming episode -->
        <upcoming-events />
        <!-- latest-episodes -->
        <latest-episodes />
      </div>
    </template>
    <template v-slot:side>
      <notification ref="Notification" v-if="isWebVersion && show == 1" />
      <detail-profile
        ref="DetailProfile"
        v-if="isWebVersion && show == 2"
        @cancel="closeProfile"
        @success="closeProfile"
      />
    </template>
  </page-content>
</template>

<script>
import CommonAmountReport from "@/components/home/AmountReport";
import UpcomingEvents from "@/components/home/UpcomingEvents";
import Notification from "@/components/home/Notification";
import LatestEpisodes from "@/components/home/LatestEpisodes.vue";
import PageContent from "@/components/PageContent";
import DetailProfile from "@/components/users/DetailProfile.vue";
import moment from "moment";

export default {
  components: {
    PageContent,
    Notification,
    CommonAmountReport,
    UpcomingEvents,
    LatestEpisodes,
    DetailProfile,
  },
  computed: {
    userData() {
      return this.$store.getters["Authen/getUser"] || [];
    },
    clinicID() {
      return this.$store.getters["Authen/getRole"].clinicID || "";
    },
    isClinicStaff() {
      return this.$store.getters["Authen/isClinicStaff"] || false;
      // return false;
    },
    reportOptions() {
      var options = [];
      for (var i = 1; i < 13; i++) {
        options.push({ text: `${this.$t("common.month")} ${i}`, value: i });
      }
      return options;
    },
  },
  data() {
    return {
      show: this.$route.query.profile ? 2 : 1,
      user: {},
      welcome: {
        message: "",
        icon: "icons/trophy.png",
      },
      monthSelected: moment().month() + 1,
      loading: false,
    };
  },
  mounted() {
    this.renderWelcome();
    this.$bus.on("openNotificationViewBooking", ({ id }) =>
      this.viewBooking(id)
    );
    this.$bus.on("openNotificationViewEpisode", ({ id }) =>
      this.viewEpisode(id)
    );
    this.$bus.on("openProfileDetail", () => (this.show = 2));
  },
  methods: {
    async renderWelcome() {
      this.welcome.message = "Have a nice day at work !";
    },
    async downloadReport() {
      let type =
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      var month = moment().set({
        month: this.monthSelected - 1,
        day: 1,
      });
      var timeStart = month.startOf("month").format("YYYY-MM-DD");
      var timeEnd = month.endOf("month").format("YYYY-MM-DD");
      var query = { timeStart, timeEnd };
      var filename = `Revenue report - ${this.$t("common.month")} ${
        this.monthSelected
      }.xlsx`;
      this.loading = true;
      await this.$httpClient.getFile(
        `/owner/clinics/${this.clinicID}/report/revenue/excel`,
        query,
        null,
        type,
        filename
      );
      this.loading = false;
    },
    viewBooking(id) {
      if (this.$refs.Notification) {
        this.$refs.Notification.openBooking(id);
      }
    },
    viewEpisode(id) {
      if (this.$refs.Notification) {
        this.$refs.Notification.openEpisode(id);
      }
    },
    closeProfile() {
      if (this.isWebVersion) {
        this.$router.push("home");
        this.show = 1;
      }
    },
  },
};
</script>

<style scoped lang="scss">
@import "./home.scss";
</style>
