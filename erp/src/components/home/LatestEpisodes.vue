<template>
  <div class="mt-2">
    <div class="title">
      <span class="text-h6 font-weight-bold d-block">
        {{ $t("home.episode") }}
      </span>
    </div>
    <div style="text-align: center" v-if="loading" class="mt-2">
      <v-progress-circular indeterminate color="#fefefe"></v-progress-circular>
    </div>
    <div v-else class="content_card mt-2">
      <content-item
        v-for="(item, index) in items"
        :key="index"
        :item="item"
        @view="view(item)"
      >
        <template v-slot:controller>
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
      </content-item>
      <h5 v-if="!items.length" class="no_data">
        {{ $t("common.notprovide") }}
      </h5>
    </div>
  </div>
</template>

<script>
import { EpisodeStatusType } from "@/plugins/contants";
import ContentItem from "@/components/cards/ContentItem";
import moment from "moment";

export default {
  components: { ContentItem },
  computed: {
    userID() {
      return this.$store.getters["Authen/getUser"].userID || "";
    },
  },
  data: () => ({
    loading: false,
    pageCount: 1,
    items: [],
    pageOptions: {
      page: 1,
      limit: 2,
    },
  }),
  mounted() {
    this.render();
  },
  methods: {
    async render() {
      this.loading = true;
      var result = await this.$httpClient.get(
        "/user/episodes",
        {},
        this.pageOptions
      );
      this.loading = false;
      if (result.error) {
        return this.showError("Get episode failed. Please try again later.");
      }
      var { items } = result;
      this.items = items.map((j) => ({
        id: j.episodeID,
        // If user login is a userCreate then using userAppoint info
        title:
          this.userID.toLowerCase() == j.records[0].userAppointID
            ? j.userAdmittedName
            : j.records[0].userAppointName,
        content: j.records[0].serviceName,
        image:
          (this.userID.toLowerCase() == j.records[0].userAppointID
            ? j.userAdmittedAvatar
            : j.records[0].userAppointAvatar) || "icons/user.png",
        message: `<span style="font-size:0.75rem">${moment(j.timeStart).format(
          "HH:mm"
        )} - ${j.clinicName}</span>`,
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
      }));
    },
    view(item) {
      // this.$bus.emit("openNotificationViewEpisode", item);
      this.$router.push(`/episode?id=${item.id}`);
    },
  },
};
</script>
