<template>
  <book-cover ref="BookCover">
    <v-list-item-title class="content_card_title text-center mb-3">
      <b>{{ $t("common.info") }} {{ $t("nav.service") }}</b>
    </v-list-item-title>
    <div style="text-align: center" v-if="loading">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </div>
    <div class="info_layout" v-else>
      <v-row class="avatar__detail mt-1">
        <v-avatar size="100px">
          <img alt="Avatar" :src="service.logo || 'icons/service.png'" />
        </v-avatar>
      </v-row>
      <h3 class="text-center">{{ service.serviceName }}</h3>
      <v-row class="name__detail mt-1 green--text">
        <h3 class="mb-1">{{ service.price }}</h3>
      </v-row>
      <v-row class="name__detail mt-3 green--text" v-if="service.isSaleOrder">
        <h5 class="mb-1">{{ $t("common.saleOrder") }}</h5>
      </v-row>
      <v-row v-if="showQr" class="avatar__detail mt-1">
        <vue-qr :text="service.serviceQrUri" :size="200"></vue-qr>
      </v-row>
      <v-row class="avatar__detail mt-3">
        <v-btn
          small
          @click="showQr = !showQr"
          elevation="0"
          color="green"
          outlined
          >{{ showQr ? $t("asset.hideQr") : $t("asset.showQr") }}</v-btn
        >
      </v-row>
      <v-list-item class="pr-2 pl-2 mb-5 mt-5">
        <v-divider></v-divider>
      </v-list-item>
      <div class="pl-3 pr-3">
        <p class="mb-5">
          <v-icon class="mr-2" color="black">mdi-image-multiple-outline</v-icon>
          <b>{{ $t("common.galery") }}:</b>
        </p>
        <image-galery
          :targetItemID="service.serviceID"
          :targetItemType="TargetItemType.Service"
        />
        <div class="mt-5">
          <v-icon class="mr-2" color="black">mdi-clipboard-text-outline</v-icon>
          <b>{{ $t("service.description") }}:</b>
          <div
            v-html="service.shortDescription"
            class="mt-3 mb-3 editor_result_content"
          ></div>
        </div>
        <p v-show="full">
          <v-icon class="mr-2" color="black">
            mdi-clipboard-file-outline
          </v-icon>
          <b>{{ $t("nav.doctor") }}:</b><br />
          <span v-show="!service.providers || !service.providers.length"></span>
        </p>
      </div>
      <div class="pl-1 pr-1" v-show="full">
        <content-item
          v-for="(item, index) in service.providers"
          :key="index"
          :item="item"
        >
          <template v-slot:controller>
            <v-btn fab small elevation="0" @click="callPhone(item.phone)">
              <v-icon> mdi-phone </v-icon>
            </v-btn>
          </template>
        </content-item>
      </div>
    </div>
  </book-cover>
</template>

<script>
import ContentItem from "@/components/cards/ContentItem.vue";
import BookCover from "@/components/BookCover.vue";
import ImageGalery from "@/components/ImageGalery.vue";
import { convertPriceString } from "@/plugins/helpers";
import { TargetItemType } from "@/plugins/contants";
import VueQr from "vue-qr";

export default {
  components: {
    ContentItem,
    BookCover,
    ImageGalery,
    VueQr,
  },
  props: ["serviceID", "full"],
  watch: {
    serviceID(id) {
      this.render(id);
    },
  },
  data() {
    return {
      service: {},
      loading: true,
      TargetItemType,
      showQr: false,
    };
  },
  mounted() {
    this.render(this.serviceID);
  },
  methods: {
    async render(id) {
      if (!id) return;
      this.$refs.BookCover.showContent();
      this.loading = true;
      var service = await this.$httpClient.get(`/owner/services/${id}`);
      if (service.error) {
        return this.showError("Cannot get service info!");
      }
      service.price = convertPriceString(service.price, true);
      service.serviceQrUri = `?serviceid=${service.serviceID}`;
      var providers = service.listProviders || [];
      this.service = {
        ...service,
        providers: providers.map((p) => ({
          id: p.userID,
          phone: p.phone,
          content: false,
          title: p.fullName,
          image: p.avatar || "/icons/user.png",
          item: p,
        })),
      };
      this.loading = false;
    },
  },
};
</script>
