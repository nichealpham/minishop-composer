<template>
  <div>
    <div style="text-align: center" v-if="loading" class="mt-2">
      <v-progress-circular indeterminate color="#fefefe"></v-progress-circular>
    </div>
    <div v-else>
      <content-list
        v-for="(item, index) in templates"
        :key="index"
        :template="item"
        @view="view"
        class="content_card mb-2"
      >
        <template v-slot:card-controller="{ item }">
          <v-icon
            color="green"
            class="mr-4"
            v-if="item.publicStatus"
            style="font-size: 20px"
            >mdi-monitor-share</v-icon
          >
          <v-avatar size="26px">
            <img alt="Avatar" :src="item.statusImage" />
          </v-avatar>
          <v-list-item-icon class="pt-2 pb-2">
            <v-btn
              @click="
                $event.stopPropagation();
                callPhone(item.phone);
              "
              small
              color="#eeeeee"
              class="service_button"
              fab
              elevation="0"
            >
              <v-icon>mdi-phone </v-icon>
            </v-btn>
          </v-list-item-icon>
        </template>
      </content-list>
      <h5 v-if="!templates.length" class="no_data mt-2">
        {{ $t("common.notprovide") }}
      </h5>
      <div v-if="pageCount > 1">
        <v-pagination
          v-model="pageOptions.page"
          class="my-4"
          :length="pageCount"
          color="#6166f5"
          @input="loadMore()"
        ></v-pagination>
      </div>
    </div>
  </div>
</template>

<script>
import { EpisodeStatusType } from "@/plugins/contants";
import ContentList from "@/components/cards/ContentList";
import moment from "moment";

export default {
  components: { ContentList },
  props: ["category"],
  watch: {
    category: {
      handler() {
        this.renderEvents({ keySearch: "" });
      },
    },
  },
  computed: {
    userID() {
      return this.$store.getters["Authen/getUser"].userID || "";
    },
  },
  data: () => ({
    query: {},
    loading: true,
    pageCount: 1,
    templates: [],
    pageOptions: {
      page: 1,
      limit: 10,
    },
  }),
  mounted() {
    this.renderEvents({ keySearch: "" });
  },
  methods: {
    groupByDates(array) {
      // this gives an object with dates as keys
      const groups = array.reduce((groups, item) => {
        const date = moment(item.timeStart).format("DD/MM/YYYY");
        if (!groups[date]) {
          groups[date] = [];
        }
        groups[date].push(item);
        return groups;
      }, {});
      // Edit: to add it in the array format instead
      const groupArrays = Object.keys(groups).map((date) => {
        return {
          date,
          episodes: groups[date],
        };
      });
      return groupArrays;
    },
    async loadMore() {
      this.loading = true;
      if (this.category == 1) {
        await this.renderEpisodeUser();
      } else {
        await this.renderEpisodeClinic();
      }
      this.loading = false;
    },
    async renderEvents({ keySearch }) {
      this.clean();
      this.query = { keySearch };
      this.loading = true;
      if (this.category == 1) {
        await this.renderEpisodeUser();
      } else {
        await this.renderEpisodeClinic();
      }
      this.loading = false;
    },
    async renderEpisodeUser() {
      var result = await this.$httpClient.get(
        "/user/episodes",
        this.query,
        this.pageOptions
      );
      if (result.error) {
        return this.showError("Get episode failed. Please try again later.");
      } else {
        this.$emit("success", result);
      }
      var { items, totals } = result;
      var groups = this.groupByDates(items);
      this.templates = this.transformGroups(groups);
      this.pageCount = Math.ceil(totals / this.pageOptions.limit);
    },
    async renderEpisodeClinic() {
      var clinicID = this.category;
      var result = await this.$httpClient.get(
        `/owner/clinics/${clinicID}/episodes`,
        this.query,
        this.pageOptions
      );
      if (result.error) {
        return this.showError("Get episode failed. Please try again later.");
      } else {
        this.$emit("success", result);
      }
      var { items, totals } = result;
      var groups = this.groupByDates(items);
      this.templates = this.transformGroups(groups);
      this.pageCount = Math.ceil(totals / this.pageOptions.limit);
    },
    transformGroups(groups) {
      return groups.map((i) => ({
        title: i.date,
        items: i.episodes.map((j) => ({
          id: j.episodeID,
          // If user login is a userCreate then using userAppoint info
          // title:
          //   this.userID.toLowerCase() == j.records[0].userAppointID
          //     ? j.userAdmittedName
          //     : j.records[0].userAppointName,
          title: j.userAdmittedName,
          content: j.publicTitle || j.records[0].serviceName,
          // image:
          //   (this.userID.toLowerCase() == j.records[0].userAppointID
          //     ? j.userAdmittedAvatar
          //     : j.records[0].userAppointAvatar) || "icons/user.png",
          image: j.userAdmittedAvatar || "icons/user.png",
          message: `<span style="font-size:0.75rem">${moment(
            j.timeStart
          ).format("HH:mm")} - <b>${j.records[0].userAppointName}</b></span> ${
            j.publicStatus
              ? `<i aria-hidden="true" style="font-size:0.85rem;margin-top:2px;" class="ml-1 v-icon notranslate mdi mdi-eye-outline theme--light green--text"></i> <span style="font-size:0.75rem;color:green;">${
                  j.viewCount || 0
                }</span>`
              : ""
          }`,
          time: moment(j.timeStart).format(),
          statusImage:
            j.statusID == EpisodeStatusType.Success
              ? "icons/tick.png"
              : j.statusID == EpisodeStatusType.Canceled
              ? "icons/close.png"
              : "icons/arrow-right.png",
          phone:
            this.userID.toLowerCase() == j.records[0].userAppointID
              ? j.userAdmittedPhone
              : j.records[0].userAppointPhone,
          publicStatus: j.publicStatus,
        })),
      }));
    },
    call(item) {
      var { phone } = item;
      if (phone) this.callPhone(phone);
    },
    view(item) {
      this.$emit("view", item.id);
    },
    clean() {
      this.query = {};
      this.pageCount = 1;
      this.templates = [];
      this.pageOptions = {
        page: 1,
        limit: 10,
      };
    },
  },
};
</script>
