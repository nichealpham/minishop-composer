<template>
  <book-cover ref="BookCover">
    <div v-show="show == 1">
      <div class="episode_wrapper">
        <v-tabs v-model="tabs" fixed-tabs icons-and-text color="#6166f5">
          <v-tab href="#exam">
            {{ $t("episode.exam") }}
            <v-icon>mdi-text-box-check-outline</v-icon>
          </v-tab>
          <v-tab href="#info">
            {{ $t("episode.info") }}
            <v-icon>mdi-folder-information-outline</v-icon>
          </v-tab>
        </v-tabs>
      </div>
      <v-btn
        icon
        @click="render()"
        fixed
        top
        right
        color="white"
        style="right:70px;top:-45px;position:absolute;"
      >
        <v-icon>mdi-refresh</v-icon>
      </v-btn>
      <div style="text-align:center" v-if="loading" class="mt-5">
        <v-progress-circular
          indeterminate
          color="#6166f5"
        ></v-progress-circular>
      </div>

      <div class="episode_detail" v-else>
        <v-tabs-items v-model="tabs">
          <v-tab-item value="exam" eager>
            <examination
              :episode="episode"
              @cancel="onCancelEpisode"
              @success="onFinishEpisode"
              @content-saved="$refs.StatusButtons.enableFinishBtn()"
              @content-unsaved="$refs.StatusButtons.disableFinishBtn()"
            />
          </v-tab-item>

          <v-tab-item value="info" eager>
            <episode-info :episode="episode" />
          </v-tab-item>
        </v-tabs-items>
      </div>
    </div>

    <status-buttons
      ref="StatusButtons"
      :episode="episode"
      @cancel="onCancelEpisode"
      @success="onFinishEpisode"
      @book-more="show = 2"
    />

    <clinic-input-booking
      ref="ClinicCreateBooking"
      v-show="show == 2"
      :patientID="episode && episode.userAdmittedID"
      @cancel="
        $refs.ClinicCreateBooking.clean();
        show = 1;
      "
    />
  </book-cover>
</template>

<script>
import BookCover from "@/components/BookCover.vue";
import EpisodeInfo from "./components/EpisodeInfo.vue";
import Examination from "./components/Examination.vue";
import StatusButtons from "./components/StatusButtons.vue";
import ClinicInputBooking from "@/components/clinics/InputBooking.vue";

export default {
  components: {
    BookCover,
    EpisodeInfo,
    Examination,
    StatusButtons,
    ClinicInputBooking,
  },
  props: ["episodeID"],
  watch: {
    episodeID() {
      this.render();
    },
  },
  mounted() {
    this.render();
  },
  data() {
    return {
      show: 1,
      tabs: "exam",
      loading: false,
      episode: null,
    };
  },
  methods: {
    async render() {
      this.clean();
      if (!this.episodeID) return;
      this.$refs.BookCover.showContent();
      this.loading = true;
      var result = await this.$httpClient.get(
        `/user/episodes/${this.episodeID}`
      );
      this.loading = false;
      if (result.error) {
        return this.showError(
          "Get episode information failed. Please try again later."
        );
      }
      this.episode = result;
    },
    clean() {
      this.show = 1;
      this.tabs = "exam";
      this.episode = null;
      this.$refs.ClinicCreateBooking.clean();
    },
    async onCancelEpisode() {
      this.$emit("cancel");
      await this.render();
    },
    async onFinishEpisode() {
      this.$emit("success");
      await this.render();
    },
  },
};
</script>

<style lang="scss">
.episode_wrapper {
  width: calc(100% + 40px);
  margin-top: -20px;
  margin-left: -20px;
  border-bottom: 1px solid #bebebe;
  .v-tab {
    font-size: 0.83rem;
  }
  .v-tabs-bar__content {
    background-color: #fafafa;
  }
  .v-item-group {
    a {
      text-transform: capitalize;
    }
  }
}
.episode_detail {
  .v-expansion-panel {
    border: 1px soild #bebebe;
    .v-expansion-panel-header {
      b {
        // text-align: center;
        text-transform: uppercase;
      }
      i {
        margin-top: -4px;
      }
    }
  }
}
</style>
