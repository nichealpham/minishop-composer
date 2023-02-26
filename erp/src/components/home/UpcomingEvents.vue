<template>
  <div class="mt-2">
    <div class="title">
      <span class="text-h6 font-weight-bold d-block">
        {{ $t("home.event") }}
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
        {{ $t("common.nodata") }}
      </h5>
    </div>
  </div>
</template>

<script>
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
    timeStart: moment().format("YYYY/MM/DD HH:mm:ss"),
    timeEnd: "",
    pageOptions: {
      page: 1,
      limit: 2,
    },
  }),
  mounted() {
    this.renderEvents({ keySearch: "" });
  },
  methods: {
    async renderEvents() {
      var query = { timeStart: this.timeStart, timeEnd: this.timeEnd };
      this.loading = true;
      var result = await this.$httpClient.get(
        "/user/appointments",
        query,
        this.pageOptions
      );
      if (result.error) {
        return this.showError(
          "Get appointment failed. Please try again later."
        );
      }
      var { items } = result;
      this.items = items.map((j) => ({
        ...j,
        id: j.appointmentID,
        title:
          this.userID.toLowerCase() == j.userCreateID
            ? j.userAppointName
            : j.userCreateName,
        image:
          (this.userID.toLowerCase() == j.userCreateID
            ? j.userAppointAvatar
            : j.userCreateAvatar) || "icons/user.png",
        content: j.serviceName,
        message: `<b><span style="font-size:13px" class="green--text">
          ${moment(j.timeStart).format("HH:mm")} - ${moment(j.timeEnd).format(
          "HH:mm"
        )}, ${moment(j.timeStart).format("DD/MM/YYYY")}</span></b>`,
        // message: j.clinicName,
        phone:
          this.userID.toLowerCase() == j.userCreateID
            ? j.userAppointPhone
            : j.userCreatePhone,
      }));
      this.loading = false;
    },
    view(item) {
      // this.$bus.emit("openNotificationViewBooking", item);
      this.$router.push(`/booking?id=${item.id}`);
    },
  },
};
</script>
