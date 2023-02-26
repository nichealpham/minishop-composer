<template>
  <book-cover ref="BookCover">
    <v-list-item-title class="content_card_title text-center mb-3">
      <b>{{ $t("common.info") + " " + $t("nav.clinic") }}</b>
    </v-list-item-title>
    <div style="text-align: center" v-show="loading">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </div>
    <div class="info_layout" v-show="!loading">
      <v-row class="avatar__detail mt-1">
        <v-avatar size="100px">
          <img alt="Avatar" :src="clinic.logo || 'icons/clinic.png'" />
        </v-avatar>
      </v-row>
      <v-row class="name__detail">
        <b class="mb-1">{{ clinic.clinicName }}</b>
      </v-row>
      <v-row class="mb-4 user_button_detail">
        <v-btn
          depressed
          color="#eeeeee"
          class="button mr-2 text-capitalize"
          style="width: 100px"
          v-if="$minishopUrl"
          @click="
            copyToClipboard($minishopUrl);
            openBrowserNewTab($minishopUrl);
          "
        >
          <v-icon left dark> mdi-web </v-icon>
          Home
        </v-btn>
        <v-btn
          depressed
          color="#eeeeee"
          class="button text-capitalize"
          :disabled="!clinic.phone"
          @click="callPhone(clinic.phone)"
          style="width: 100px"
        >
          <v-icon left dark> mdi-phone </v-icon>
          {{ $t("user.call") }}
        </v-btn>
      </v-row>
      <v-list-item class="pr-2 pl-2 mb-5">
        <v-divider></v-divider>
      </v-list-item>
      <div class="pl-1 pr-1">
        <p>
          <v-icon class="mr-2" color="black">
            mdi-map-marker-radius-outline
          </v-icon>
          <b>{{ $t("user.address") }}:</b>
          {{ clinic.address }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-clipboard-text-outline</v-icon>
          <b>{{ $t("clinic.description") }}:</b>
          {{ clinic.shortDescription }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-file-code-outline</v-icon>
          <b>{{ $t("clinic.tax") }}:</b>
          {{ clinic.taxNumber }}
        </p>
        <p v-if="clinic.publicNameUrl">
          <v-icon class="mr-2" color="black">mdi-web</v-icon>
          <b>Website:</b>
          <a class="ml-1" @click="openBrowserNewTab(clinic.publicNameUrl)">{{
            clinic.publicNameUrl
          }}</a>
        </p>
        <p v-show="full">
          <v-icon class="mr-2" color="black">
            mdi-clipboard-file-outline
          </v-icon>
          <b>{{ $t("cardTitle.services") }}:</b><br />
          <span v-show="!clinic.services || !clinic.services.length"></span>
        </p>
      </div>
      <div class="pl-1 pr-1" v-show="full">
        <content-item
          v-for="(item, index) in clinic.services"
          :key="index"
          :item="item"
        >
        </content-item>
      </div>
    </div>
  </book-cover>
</template>

<script>
import ContentItem from "@/components/cards/ContentItem.vue";
import BookCover from "@/components/BookCover.vue";
import { convertPriceString } from "@/plugins/helpers";

export default {
  components: { ContentItem, BookCover },
  props: ["clinicID", "full"],
  watch: {
    clinicID(id) {
      this.render(id);
    },
  },
  data() {
    return {
      clinic: {},
      loading: false,
    };
  },
  mounted() {
    this.render(this.clinicID);
  },
  methods: {
    hideCover() {
      this.$refs.BookCover.showContent();
    },
    async render(id) {
      if (!id) return;
      this.loading = true;
      var clinic = await this.$httpClient.get(`/owner/clinics/${id}`);
      if (clinic.error) {
        return this.showError("Cannot get clinic profile!");
      }
      var services = clinic.services || [];
      Object.keys(clinic).forEach((key) => {
        if (key == "logo") return;
        if (!clinic[key]) clinic[key] = this.$t("common.notprovide");
      });
      this.clinic = {
        ...clinic,
        services: services.map((s) => ({
          id: s.serviceID,
          title: s.serviceName,
          content: convertPriceString(s.price),
          image: s.logo || "/icons/service.png",
          item: s,
        })),
      };
      this.loading = false;
    },
  },
};
</script>
