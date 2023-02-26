<template>
  <book-cover ref="BookCover">
    <v-list-item-title class="content_card_title text-center mb-3">
      <b>{{ $t("cardTitle.assets") }}</b>
    </v-list-item-title>
    <div style="text-align: center" v-if="loading">
      <v-progress-circular indeterminate color="#6166f5"></v-progress-circular>
    </div>
    <div class="info_layout" v-else>
      <h4 class="text-center green--text mb-1" v-if="asset.isSale">
        {{ $t("common.forSale") }}
      </h4>
      <h2 class="text-center">
        {{ asset.assetName }}
      </h2>
      <v-row
        class="name__detail mt-2 green--text"
        v-if="asset.type == AssetType.Consumable"
      >
        <h3 class="mb-1">{{ $t("asset.instock") }}: {{ asset.amount }}</h3>
      </v-row>
      <v-row v-if="showQr" class="avatar__detail mt-1">
        <vue-qr :text="asset.assetQrUri" :size="200"></vue-qr>
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
      <div class="pl-3 pr-3 mb-5"></div>
      <div class="pl-3 pr-3">
        <p class="mb-5" v-if="!$isPatient">
          <v-icon class="mr-2" color="black">mdi-image-multiple-outline</v-icon>
          <b>{{ $t("common.galery") }}:</b>
        </p>
        <image-galery
          v-if="
            !$isPatient &&
            asset.clinicID &&
            $clinicID.toLowerCase() == asset.clinicID.toLowerCase()
          "
          :targetItemID="asset.assetID"
          :targetItemType="TargetItemType.Asset"
        />
        <div
          class="mt-5"
          v-if="
            !$isPatient &&
            asset.clinicID &&
            $clinicID.toLowerCase() == asset.clinicID.toLowerCase()
          "
        >
          <v-icon class="mr-2" color="black">mdi-tag-multiple-outline</v-icon>
          <b>{{ $t("common.category") }}:</b>
          <div class="mt-0">
            <category-selector
              :targetItemID="asset.assetID"
              :targetItemType="TargetItemType.Asset"
            />
          </div>
        </div>
        <p class="mt-5">
          <v-icon class="mr-2" color="black">mdi-clipboard-file-outline</v-icon>
          <b>{{ $t("asset.type") }}:</b>
          {{ asset.typeName }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-file-code-outline</v-icon>
          <b>{{ $t("asset.assetCode") }}:</b>
          {{ asset.assetCode }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-cash</v-icon>
          <b>{{ $t("asset.price") }}:</b>
          <b class="green--text ml-1">{{ asset.price }}</b>
        </p>
        <div v-if="asset.saleDescription" class="mb-3">
          <v-icon class="mr-2" color="black">mdi-file-code-outline</v-icon>
          <b>{{ $t("common.saleHighlight") }}:</b>
          <div
            v-html="asset.saleDescription"
            class="mt-3 mb-3 editor_result_content"
          ></div>
        </div>
        <div v-if="asset.description">
          <v-icon class="mr-2" color="black">mdi-clipboard-text-outline</v-icon>
          <b>{{ $t("asset.description") }}:</b>
          <div
            v-html="asset.description"
            class="mt-3 mb-3 editor_result_content"
          ></div>
        </div>
      </div>
    </div>
  </book-cover>
</template>

<script>
import VueQr from "vue-qr";
import BookCover from "@/components/BookCover.vue";
import { convertPriceString } from "@/plugins/helpers";
import { AssetType, TargetItemType } from "@/plugins/contants";
import ImageGalery from "@/components/ImageGalery.vue";
import CategorySelector from "@/components/CategorySelector.vue";

export default {
  components: { BookCover, VueQr, ImageGalery, CategorySelector },
  props: ["assetID"],
  watch: {
    assetID(id) {
      this.render(id);
    },
  },
  data() {
    return {
      asset: {},
      loading: true,
      showQr: false,
      AssetType,
      TargetItemType,
    };
  },
  mounted() {
    this.render(this.assetID);
  },
  methods: {
    async render(id) {
      if (!id) return;
      this.$refs.BookCover.showContent();
      this.loading = true;
      var asset = await this.$httpClient.get(`/owner/assets/${id}`);
      console.log(asset);
      console.log(this.$clinicID);
      if (asset.error) {
        return this.showError("Cannot get asset info!");
      }
      asset.price = asset.price
        ? convertPriceString(asset.price, true)
        : this.$t("common.notprovide");
      asset.typeName =
        asset.type == AssetType.Consumable
          ? this.$t("asset.consumable")
          : this.$t("asset.asset");
      asset.assetCode = asset.assetCode || this.$t("common.notprovide");
      this.asset = asset;
      this.loading = false;
    },
  },
};
</script>
