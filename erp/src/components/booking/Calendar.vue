<template>
  <div class="calendar">
    <v-row class="fill-height">
      <v-col>
        <v-sheet height="64">
          <v-toolbar flat>
            <v-btn
              outlined
              class="mr-4"
              color="grey darken-2"
              @click="setToday"
            >
              {{ $t("common.today") }}
            </v-btn>
            <v-btn fab text small color="grey darken-2" @click="prev">
              <v-icon small> mdi-chevron-left </v-icon>
            </v-btn>
            <v-btn fab text small color="grey darken-2" @click="next">
              <v-icon small> mdi-chevron-right </v-icon>
            </v-btn>
            <v-toolbar-title v-if="$refs.calendar" class="ml-2 calendar_title">
              {{ $refs.calendar.title }}
            </v-toolbar-title>
            <v-spacer />
            <v-progress-circular
              v-if="loading"
              indeterminate
              color="#6166f5"
              size="24"
            ></v-progress-circular>
          </v-toolbar>
        </v-sheet>
        <v-sheet height="560">
          <v-calendar
            ref="calendar"
            v-model="focus"
            :events="events"
            :type="type"
            :event-overlap-mode="'column'"
            :event-overlap-threshold="30"
            @click:event="showEvent"
            @click:more="viewDay"
            @click:date="viewDay"
            @change="updateRange"
            locale="vn"
          >
            <template v-slot:day-body="{ date, week }">
              <div
                class="v-current-time"
                :class="{ first: date === week[0].date }"
                :style="{ top: nowY }"
              ></div>
            </template>
            <!-- <template #event="{ event }">
              <p>
                <v-icon x-small color="white" class="mr-1">
                  mdi-account-outline
                </v-icon>
                <b>{{ event.service }}</b>
                {{ event.name }}
              </p>
            </template> -->
          </v-calendar>
        </v-sheet>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import moment from "moment";
export default {
  props: ["category", "viewType", "keySearch"],
  // category booking
  watch: {
    category: {
      handler() {
        this.renderEvents();
      },
    },
  },
  computed: {
    cal() {
      return this.ready ? this.$refs.calendar : null;
    },
    nowY() {
      return this.cal ? this.cal.timeToY(this.cal.times.now) + "px" : "-10px";
    },
    type() {
      if (this.viewType == "month") {
        return "month";
      }
      if (this.viewType == "4day") {
        return "4day";
      }
      if (this.isMobileVersion) {
        return "day";
      } else {
        // return "4day";
        return "day";
      }
    },
    userID() {
      return this.$store.getters["Authen/getUser"].userID || "";
    },
  },
  data: () => ({
    ready: false,
    loading: false,
    timeStart: null,
    timeEnd: null,
    events: [],
    clinics: [],
    focus: "",
    // colors: [
    //   "blue",
    //   "#6166f5",
    //   "cyan",
    //   "green",
    //   "orange lighten-2",
    //   "grey darken-1",
    // ],
    colors: ["#6166f5", "#6166f5", "#6166f5", "#6166f5", "#6166f5", "#6166f5"],
    pageOptions: {
      page: 1,
      limit: 1000,
    },
    typeToLabel: {
      month: "Month",
      week: "Week",
      day: "Day",
      "4day": "4 Days",
    },
  }),
  mounted() {
    this.ready = true;
    this.$refs.calendar.scrollToTime(moment().format("HH:mm"));
    this.updateTime();
  },
  methods: {
    updateTime() {
      setInterval(() => this.cal.updateTimes(), 60 * 1000);
    },
    viewDay({ date }) {
      this.focus = date;
      this.type = "week";
    },
    setToday() {
      this.focus = "";
    },
    prev() {
      this.$refs.calendar.prev();
    },
    next() {
      this.$refs.calendar.next();
    },
    showEvent(ev) {
      this.$emit("showEvent", ev.event.id);
    },
    async updateRange({ start, end }) {
      this.timeStart = start.date;
      this.timeEnd = end.date;
      this.renderEvents();
    },
    async renderEvents() {
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
        keySearch: this.keySearch,
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
      this.events = result.items.map((i) => ({
        // name:
        //   this.userID.toLowerCase() == i.userCreateID
        //     ? i.userAppointName
        //     : i.userCreateName,
        name: `${i.serviceName} - ${i.userAppointName}`,
        service: i.serviceName,
        start: moment(i.timeStart).format("YYYY-MM-DD HH:mm"),
        end: moment(i.timeEnd).format("YYYY-MM-DD HH:mm"),
        id: i.appointmentID,
        color:
          moment().format("YYYY-MM-DD HH:mm") >
          moment(i.timeEnd).format("YYYY-MM-DD HH:mm")
            ? this.colors[4]
            : this.colors[1],
      }));
    },
    async renderBookingClinic() {
      var clinicID = this.category;
      const query = {
        timeStart: this.timeStart,
        timeEnd: this.timeEnd,
        keySearch: this.keySearch,
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
      this.events = result.items.map((i) => ({
        // name:
        //   this.userID.toLowerCase() == i.userCreateID
        //     ? i.userAppointName
        //     : i.userCreateName,
        name: `${i.serviceName} - ${i.userAppointName}`,
        service: i.serviceName,
        start: moment(i.timeStart).format("YYYY-MM-DD HH:mm"),
        end: moment(i.timeEnd).format("YYYY-MM-DD HH:mm"),
        id: i.appointmentID,
        color:
          moment().format("YYYY-MM-DD HH:mm") >
          moment(i.timeEnd).format("YYYY-MM-DD HH:mm")
            ? this.colors[4]
            : this.colors[1],
      }));
    },
  },
};
</script>
<style lang="scss">
.calendar {
  .calendar_title {
    font-size: 1.15rem;
    @media screen and (max-width: 819px) {
      font-size: 1rem;
    }
  }
  background-color: rgba(245, 245, 245, 0.5) !important;
  .theme--light.v-sheet {
    background-color: transparent;
    * {
      border-color: #f5f5f5 !important;
    }
  }
  .theme--light.v-toolbar.v-sheet {
    background-color: rgba(245, 245, 245, 0.9) !important;
  }
  .v-calendar {
    background-color: rgba(245, 245, 245, 0.7) !important;
    @media screen and (max-width: 819px) {
      margin-top: -9px;
    }
    b,
    strong {
      font-weight: 400;
    }
    .v-btn--fab.v-size--default {
      height: 24px;
      width: 24px;
    }
    .v-event-timed-container {
      margin: -2px !important;
      @media screen and (max-width: 819px) {
        margin: 0px !important;
      }
      .v-event-timed {
        * {
          font-size: 10px;
        }
        padding: 7px;
        p {
          padding: 0px;
          margin: 0px;
          margin-bottom: 2px;
        }
      }
    }
  }
}
</style>
