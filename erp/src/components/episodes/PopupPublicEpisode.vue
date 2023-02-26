<template>
  <v-dialog v-model="showDialog" max-width="700px" eager>
    <v-card>
      <div class="info_layout pa-5">
        <div class="title_area">
          <div class="user__detail mb-10">
            <div style="text-align:center" v-if="loading">
              <v-progress-circular
                indeterminate
                color="#6166f5"
              ></v-progress-circular>
            </div>
            <div v-else class="editor_result_content">
              <b class="service_title">{{ episode.publicTitle }}</b>
              <div v-html="episode.publicHeadline" class="mb-3"></div>
              <div v-html="episode.publicDetail" class="mb-3"></div>
            </div>
          </div>
        </div>
      </div>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  computed: {
    episodeNameUrl() {
      return this.$store.getters["Public/getEpisodeName"] || "";
    },
  },
  watch: {
    episodeNameUrl(val) {
      if (val) {
        this.render();
      }
    },
    showDialog(val) {
      if (!val) {
        this.clean();
      }
    },
  },
  data() {
    return {
      loading: false,
      showDialog: false,
      episode: {},
    };
  },
  created() {},
  methods: {
    async render() {
      if (this.episodeNameUrl) {
        this.showDialog = true;
        this.loading = true;
        var result = await this.$httpClient.get(
          `/public/episode/${this.episodeNameUrl}`
        );
        this.loading = false;
        if (!result || result.error) return this.clean();
        this.episode = result;
      }
    },
    clean() {
      this.showDialog = false;
      this.loading = false;
      this.episode = {};
      this.$store.commit("Public/SET_EPISODE_NAME", null);
    },
  },
};
</script>
