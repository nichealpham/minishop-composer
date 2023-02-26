<template>
  <book-cover ref="BookCover" :message="$t('common.awesomenodata')">
    <div class="notification_wrapper">
      <v-list-item-title class="content_card_title text-center mb-3">
        <b>{{ $t("home.notification") }}</b>
        <v-btn icon @click="render()" fixed top right>
          <v-icon>mdi-refresh</v-icon>
        </v-btn>
      </v-list-item-title>
      <div style="text-align:center" v-if="loading">
        <v-progress-circular
          indeterminate
          color="#605bff"
        ></v-progress-circular>
      </div>
      <div v-else>
        <!-- End info -->
        <div v-for="(item, index) in notifications" :key="index">
          <p class="mb-1">
            <b class="noti_title">{{ item.title }}</b>
          </p>
          <v-timeline align-top dense>
            <v-timeline-item
              v-for="(item, i) in item.content"
              :key="i"
              :color="item.isRead ? '#424242' : item.color"
              :icon="item.icon"
              fill-dot
            >
              <v-alert
                v-ripple
                :value="true"
                :color="item.color"
                class="white--text"
                @click="updateIsRead(item)"
                :class="{ is_read: true }"
              >
                <p v-html="item.message"></p>
                <p>
                  <v-icon small class="mr-1">mdi-clock-outline</v-icon>
                  {{ $t("common.at") }} {{ item.time }}
                </p>
              </v-alert>
              <!-- <v-badge
                :color="item.statusCode == 0 ? '#fafafa' : 'pink'"
                dot
                v-if="!item.isRead"
              ></v-badge> -->
            </v-timeline-item>
          </v-timeline>
        </div>
        <div v-if="pageCount > 1">
          <v-pagination
            v-model="pageOptions.page"
            :length="pageCount"
            color="#6166f5"
            @input="loadMore()"
            class="my-2"
          ></v-pagination>
        </div>
      </div>
    </div>
  </book-cover>
</template>

<script>
import BookCover from "@/components/BookCover.vue";
import moment from "moment";
import { TransformNotificationItem } from "@/plugins/contants";

export default {
  components: { BookCover },
  computed: {
    userID() {
      return this.$store.getters["Authen/getUser"].userID.toLowerCase();
    },
    fcmNew() {
      return this.$store.getters["Fcm/new"];
    },
  },
  watch: {
    fcmNew(val, old) {
      if (val && val != old) this.render();
    },
  },
  data() {
    return {
      pageCount: 1,
      pageOptions: {
        page: 1,
        limit: 7,
      },
      loading: false,
      notifications: [],
    };
  },
  mounted() {
    this.render();
  },
  methods: {
    groupByDates(array) {
      // this gives an object with dates as keys
      const groups = array.reduce((groups, item) => {
        var date = moment(item.dateCreated).format("DD/MM/YYYY");
        if (date == moment().format("DD/MM/YYYY")) {
          date = `${this.$t("common.today")}, ${date}`;
        }
        if (!groups[date]) {
          groups[date] = [];
        }
        groups[date].push(this.transformItem(item));
        return groups;
      }, {});
      // Edit: to add it in the array format instead
      const groupArrays = Object.keys(groups).map((date) => {
        return {
          title: date,
          content: groups[date],
        };
      });
      return groupArrays;
    },
    transformItem: TransformNotificationItem,
    async render() {
      this.clean();
      this.renderListNotifications();
    },
    async loadMore() {
      this.renderListNotifications();
    },
    async renderListNotifications() {
      this.$refs.BookCover.showContent();
      this.loading = true;
      var { items, error, totals } = await this.$httpClient.get(
        `/user/activities`,
        null,
        this.pageOptions
      );
      this.loading = false;
      if (error) {
        this.$refs.BookCover.showCover();
        this.showError("get activities failed. Please try again later");
        return;
      }
      if (items.length == 0) {
        this.$refs.BookCover.showCover();
        return;
      }
      this.notifications = this.groupByDates(items);
      this.pageCount = Math.ceil(totals / this.pageOptions.limit);
    },
    async updateIsRead(item) {
      var { id } = item;
      this.$emit("view", item);
      await this.$httpClient.put(`/user/activities/${id}`);
      this.$store.commit("Fcm/SET_COUNT");
      this.renderListNotifications();
    },
    clean() {
      this.pageCount = 1;
      this.pageOptions = {
        page: 1,
        limit: 7,
      };
    },
  },
};
</script>

<style lang="scss">
.notification_wrapper {
  .noti_title {
    display: inline-block;
    text-align: center;
    font-size: 0.9rem;
    font-weight: 600;
  }
  .v-timeline--dense:not(.v-timeline--reverse):before {
    left: calc(48px - 24px) !important;
    opacity: 0.8;
    background: #bdbdbd;
    width: 1px;
  }
  .v-timeline {
    padding-top: 12px;
    width: calc(100% + 45px);
    .v-timeline-item {
      .is_read {
        background-color: #f5f5f5 !important;
        color: #212121 !important;
      }
      padding-bottom: 16px;
      .v-timeline-item__dot {
        box-shadow: none;
      }
      .v-timeline-item__divider {
        min-width: 52px;
        left: -16px;
      }
      .v-timeline-item__body {
        margin-top: -15px;
        left: -12px;
        margin-right: 30px;
        .v-alert__wrapper {
          p {
            padding: 0px;
            margin: 0px;
            font-size: 1rem;
          }
          p:last-child {
            margin-top: 5px;
            font-size: 0.9rem;
          }
        }
        .v-badge {
          position: absolute;
          top: 15px;
          right: 15px;
        }
      }
    }
    .v-timeline-item:last-child {
      padding-bottom: 0px;
    }
    .v-timeline-item:first-child {
      padding-top: 12px;
    }
  }
}
</style>
