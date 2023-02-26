<template>
  <div>
    <notification-detail
      ref="NotificationDetail"
      v-show="show == 1"
      @view="viewNotification"
    />
    <div v-if="show != 1" class="btn_back_wraper" @click="show = 1">
      <v-btn icon>
        <v-icon>mdi-arrow-left</v-icon>
      </v-btn>
      {{ $t("common.back") }}
    </div>
    <div class="mt-2">
      <detail-booking
        ref="BookingDetail"
        :bookingID="bookingID"
        v-show="show == 2"
        @cancel="show = 1"
        @checkedin="show = 1"
      />
    </div>
    <div class="mt-5">
      <episode-detail
        ref="EpisodeDetail"
        :episodeID="episodeID"
        v-show="show == 3"
        @cancel="show = 1"
        @success="show = 1"
      />
    </div>
  </div>
</template>

<script>
import DetailBooking from "@/components/booking/DetailBooking.vue";
import NotificationDetail from "./NotificationDetail.vue";
import EpisodeDetail from "@/components/episodes/EpisodeDetail.vue";
import { ServiceCode } from "@/plugins/contants";

export default {
  components: { NotificationDetail, DetailBooking, EpisodeDetail },
  props: {},
  computed: {
    userID() {
      return this.$store.getters["Authen/getUser"].userID.toLowerCase();
    },
  },
  watch: {},
  data() {
    return {
      bookingID: null,
      episodeID: null,
      show: 1,
    };
  },
  mounted() {},
  methods: {
    render() {
      this.clean();
      this.$refs.NotificationDetail.render();
    },
    viewNotification({ serviceCode, targetItemID }) {
      this.clean();
      if (serviceCode == ServiceCode.Booking) {
        this.bookingID = targetItemID;
        this.show = 2;
      }
      if (serviceCode == ServiceCode.Episode) {
        this.episodeID = targetItemID;
        this.show = 3;
      }
    },
    openBooking(id) {
      if (this.bookingID == id) {
        this.$refs.BookingDetail.render();
      } else {
        this.bookingID = id;
      }
      this.show = 2;
    },
    openEpisode(id) {
      if (this.episodeID == id) {
        this.$refs.EpisodeDetail.render();
      } else {
        this.episodeID = id;
      }
      this.show = 3;
    },
    clean() {
      this.bookingID = null;
      this.episodeID = null;
      this.show = 1;
    },
  },
};
</script>

<style lang="scss">
.btn_back_wraper {
  margin-top: -20px;
  width: calc(100% + 40px);
  margin-left: -20px;
  background-color: #fafafa;
  padding: 0px 20px;
}
</style>
