<template>
  <page-content>
    <template v-slot:toolbar>
      <div class="fit_toolbar_booking">
        <v-list-item class="pa-0">
          <v-card-text class="pa-0">
            <v-text-field
              :placeholder="$t('common.search')"
              outlined
              dense
              background-color="white"
              append-icon="mdi-magnify"
              class="mt-0 pt-0"
              v-model="keySearch"
              v-on:keyup.enter="searchEpisode"
            ></v-text-field>
          </v-card-text>
          <v-select
            class="ml-1"
            :items="categories"
            v-model="category"
            outlined
            dense
            background-color="white"
          ></v-select>
        </v-list-item>
      </div>
    </template>

    <template v-slot:main>
      <list-episode
        ref="ListEpisode"
        :category="category"
        @view="openEpisodeProfile"
      />
    </template>

    <template v-slot:side>
      <episode-detail
        ref="EpisodeDetail"
        :episodeID="episodeID"
        @cancel="searchEpisode()"
        @success="searchEpisode()"
      />
    </template>
  </page-content>
</template>

<script>
import PageContent from "@/components/PageContent.vue";
import ListEpisode from "@/components/episodes/ListEpisode";
import EpisodeDetail from "@/components/episodes/EpisodeDetail.vue";
// import { RoleType } from "@/plugins/contants";

export default {
  components: {
    PageContent,
    ListEpisode,
    EpisodeDetail,
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
          text: this.$t("episode.myepisode"),
          value: 1,
        },
        ...clinics,
      ];
    },
  },
  data() {
    return {
      category: 1,
      keySearch: "",
      episodeID: "",
    };
  },
  mounted() {
    if (this.$route.query.id) {
      setTimeout(() => {
        this.openEpisodeProfile(this.$route.query.id);
      }, 1000);
    }
  },
  methods: {
    openEpisodeProfile(episodeID) {
      this.episodeID = episodeID;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.episode")
      );
    },
    searchEpisode() {
      this.$refs.ListEpisode.renderEvents({ keySearch: this.keySearch });
    },
  },
};
</script>
