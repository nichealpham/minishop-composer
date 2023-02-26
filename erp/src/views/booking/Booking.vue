<template>
  <page-content>
    <template v-slot:toolbar>
      <div class="fit_toolbar_booking">
        <v-text-field
          :placeholder="$t('common.search')"
          outlined
          dense
          background-color="white"
          append-icon="mdi-magnify"
          class="mt-0 pt-0 mr-1"
          v-model="keySearch"
          v-on:keyup.enter="searchBookings"
          style="min-width: 160px"
        ></v-text-field>
        <v-btn-toggle
          v-model="viewType"
          color="primary"
          borderless
          mandatory
          class="toggle_button_booking"
        >
          <!-- <v-btn value="list" class="toggle_button_booking">
            <v-icon small>mdi-format-list-bulleted </v-icon>
          </v-btn> -->
          <v-btn value="month" class="toggle_button_booking">
            <v-icon small>mdi-calendar-month-outline</v-icon>
          </v-btn>
          <v-btn value="4day" class="toggle_button_booking">
            <v-icon small>mdi-format-list-bulleted </v-icon>
          </v-btn>
          <v-btn value="day" class="toggle_button_booking">
            <v-icon small>mdi-table-column</v-icon>
          </v-btn>
        </v-btn-toggle>
        <v-select
          class="ml-1"
          :items="categories"
          v-model="category"
          outlined
          dense
          background-color="white"
          style="max-width: 120px"
        ></v-select>
      </div>
    </template>

    <template v-slot:main>
      <list-booking
        ref="ListBooking"
        @view="openBookingDetail"
        :category="category"
        v-if="viewType == 'list'"
      />
      <calendar
        ref="CanlendarBooking"
        v-else
        class="content_card"
        :keySearch="keySearch"
        :category="category"
        :class="{ 'mt-1': isMobileVersion }"
        :viewType="viewType"
        @showEvent="openBookingDetail"
      />
    </template>

    <template v-slot:side>
      <detail-booking
        :bookingID="bookingID"
        v-show="show == 1"
        @cancel="onCancelBooking"
      />
    </template>
  </page-content>
</template>

<script>
import Calendar from "@/components/booking/Calendar";
import ListBooking from "@/components/booking/ListBooking";
import PageContent from "@/components/PageContent.vue";
import DetailBooking from "@/components/booking/DetailBooking";
// import { RoleType } from "@/plugins/contants";

export default {
  components: {
    PageContent,
    DetailBooking,
    Calendar,
    ListBooking,
  },
  computed: {
    categories() {
      var clinics = this.$store.getters["Authen/getUser"].roles
        // .filter((r) => r.roleType == RoleType.Owner)
        .map((role) => ({
          value: role.clinicID,
          text: role.clinicName,
        }));
      return [
        {
          text: this.$t("booking.myappointment"),
          value: 1,
        },
        ...clinics,
      ];
    },
  },
  data() {
    return {
      show: 1,
      category: 1,
      viewType: "day",
      bookingID: "",
      keySearch: "",
    };
  },
  mounted() {
    if (this.$route.query.id) {
      setTimeout(() => {
        this.openBookingDetail(this.$route.query.id);
      }, 1000);
    }
  },
  methods: {
    searchBookings() {
      this.$refs.CanlendarBooking.renderEvents();
    },
    openBookingDetail(id) {
      this.show = 1;
      this.bookingID = id;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.booking")
      );
    },
    onCancelBooking() {
      this.bookingID = "";
      this.closeDiaglogIfMobile();
      if (this.viewType == "day") {
        this.$refs.CanlendarBooking.renderEvents();
      } else {
        this.$refs.ListBooking.renderEvents();
      }
    },
  },
};
</script>
