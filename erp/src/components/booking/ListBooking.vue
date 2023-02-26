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
          <v-list-item-icon class="pt-2 pb-2">
            <h4 class="green--text mt-2 mr-2">{{ item.time }}</h4>
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
import ContentList from "@/components/cards/ContentList";
import moment from "moment";

export default {
  components: { ContentList },
  props: ["category"],
  // category booking
  watch: {
    category: {
      handler() {
        this.renderEvents();
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
    timeStart: moment().format("YYYY/MM/DD HH:mm:ss"),
    timeEnd: "",
    pageOptions: {
      page: 1,
      limit: 10,
    },
  }),
  mounted() {
    this.renderEvents();
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
          bookings: groups[date],
        };
      });
      return groupArrays;
    },
    async loadMore() {
      this.loading = true;
      if (this.category == 1) {
        await this.renderBookingUser();
      } else {
        await this.renderBookingClinic();
      }
      this.loading = false;
    },
    async renderEvents() {
      this.clean();
      this.loading = true;
      if (this.category == 1) {
        await this.renderBookingUser();
      } else {
        await this.renderBookingClinic();
      }
      this.loading = false;
    },
    async renderBookingUser() {
      const query = {
        timeStart: this.timeStart,
        timeEnd: this.timeEnd,
      };
      var result = await this.$httpClient.get(
        "/user/appointments",
        query,
        this.pageOptions
      );
      if (result.error) {
        return this.showError(
          "Get appointment failed. Please try again later."
        );
      } else {
        this.$emit("success", result);
      }
      var { items, totals } = result;
      var groups = this.groupByDates(items);
      this.templates = this.transformGroups(groups);
      this.pageCount = Math.ceil(totals / this.pageOptions.limit);
    },
    async renderBookingClinic() {
      var clinicID = this.category;
      const query = {
        timeStart: this.timeStart,
        timeEnd: this.timeEnd,
      };
      var result = await this.$httpClient.get(
        `/owner/clinics/${clinicID}/appointments`,
        query,
        this.pageOptions
      );
      if (result.error) {
        return this.showError(
          "Get appointment failed. Please try again later."
        );
      } else {
        this.$emit("success", result);
      }
      var { items, totals } = result;
      var groups = this.groupByDates(items);
      this.templates = this.transformGroups(groups);
      this.pageCount = Math.ceil(totals / this.pageOptions.limit);
    },
    transformGroups(items = []) {
      return items.map((i) => ({
        title: i.date,
        items: i.bookings.map((j) => ({
          id: j.appointmentID,
          // If user login is a userCreate then using userAppoint info
          title:
            this.userID.toLowerCase() == j.userCreateID
              ? j.userAppointName
              : j.userCreateName,
          image:
            (this.userID.toLowerCase() == j.userCreateID
              ? j.userAppointAvatar
              : j.userCreateAvatar) || "icons/user.png",
          content: j.serviceName,
          time: this.isMobileVersion
            ? moment(j.timeStart).format("HH:mm")
            : `${moment(j.timeStart).format("HH:mm")} - ${moment(
                j.timeEnd
              ).format("HH:mm")}`,
          message: `<span style="font-size:13px">${j.clinicName}</span>`,
          phone:
            this.userID.toLowerCase() == j.userCreateID
              ? j.userAppointPhone
              : j.userCreatePhone,
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
