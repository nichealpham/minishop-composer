<template>
  <div>
    <div style="text-align:center" v-if="loading">
      <v-progress-circular indeterminate color="#6166f5"></v-progress-circular>
    </div>
    <div v-else>
      <div class="w-100" v-if="episode.publicStatus" style="margin-top:-15px;">
        <vue-qr
          :text="
            `${episode.clinic && episode.clinic.publicNameUrl}?episodeid=${
              episode.publicNameUrl
            }`
          "
          :size="200"
        ></vue-qr>
      </div>
      <div
        class="w-100 mb-2"
        v-if="episode.publicStatus"
        style="margin-top:-10px;"
      >
        <v-btn
          small
          elevation="0"
          color="grey lighten-3"
          @click="
            openBrowserNewTab(
              `${episode.clinic && episode.clinic.publicNameUrl}/blogs/${
                episode.publicNameUrl
              }`
            )
          "
          class="text-capitalize"
        >
          {{ $t("common.open") }}</v-btn
        >
      </div>
      <div class="w-100">
        <v-btn
          small
          elevation="0"
          :color="episode.publicStatus ? 'orange' : 'green'"
          outlined
          @click="toggleShareStatus"
          class="text-capitalize"
          >{{
            episode.publicStatus ? $t("common.unshare") : $t("common.share")
          }}
          {{ $t("nav.episode") }}</v-btn
        >
      </div>
    </div>
    <popup-confirm
      ref="PopupConfirm"
      :title="confirm.title"
      :message="confirm.message"
    />
  </div>
</template>

<script>
import VueQr from "vue-qr";
import PopupConfirm from "@/components/PopupConfirm.vue";

export default {
  components: { VueQr, PopupConfirm },
  props: {
    episode: {
      type: Object,
      default: function() {
        return {};
      },
    },
  },
  data() {
    return {
      loading: false,
      showQr: false,
      confirm: {
        title: "",
        message: "",
      },
    };
  },
  mounted() {},
  methods: {
    async toggleShareStatus() {
      if (!this.episode) return;
      var { clinicID, episodeID } = this.episode;
      if (!clinicID || !episodeID) return;
      var body = {
        status: !this.episode.publicStatus,
      };

      if (body.status) {
        this.confirm = {
          title: this.$t("common.share") + " " + this.$t("nav.episode"),
          message: this.$t("common.confirmShare"),
        };
      } else {
        this.confirm = {
          title: this.$t("common.unshare") + " " + this.$t("nav.episode"),
          message: this.$t("common.confirmUnshare"),
        };
      }
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      this.loading = true;
      var result = await this.$httpClient.put(
        `/Owner/Clinics/${clinicID}/Episodes/${episodeID}/Public`,
        {},
        body
      );
      this.loading = false;
      if (result && result.error) {
        return;
      }
      this.episode.publicStatus = body.status;
      this.episode.publicNameUrl = result;
    },
    clean() {
      this.loading = false;
      this.showQr = false;
    },
  },
};
</script>
