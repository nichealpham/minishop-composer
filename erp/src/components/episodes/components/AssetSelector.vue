<template>
  <div>
    <div
      class="btn_back_wraper"
      @click="$emit('back')"
      style="background-color:white;"
    >
      <v-btn icon>
        <v-icon>mdi-arrow-left</v-icon>
      </v-btn>
      <span>{{ $t("common.back") }}</span>
    </div>
    <div class="pl-1 pr-1 mt-1">
      <v-text-field
        v-model="keySearch"
        :placeholder="$t('common.search')"
        outlined
        full-width
        dense
        hide-details
        background-color="white"
        append-icon="mdi-magnify"
        class="mt-0 pt-0 mb-4"
        v-on:keyup.enter="search"
      />
      <div style="text-align:center" v-if="loading">
        <v-progress-circular
          indeterminate
          color="#6166f5"
        ></v-progress-circular>
      </div>
      <div v-else>
        <content-item
          v-for="(item, index) in options"
          :key="index"
          :item="item"
          @view="viewAsset(item.id)"
        >
          <template v-slot:controller>
            <h4 class="mr-3">
              <span style="font-weight:400;" v-if="!isMobileVersion"
                >{{ $t("asset.instock") }}:</span
              >
              {{ item.amount }}
            </h4>

            <v-btn
              fab
              small
              elevation="0"
              @click="
                $event.stopPropagation();
                $refs.PopupConsumeAsset.openCreate(item.id);
              "
            >
              <v-icon>
                mdi-plus
              </v-icon>
            </v-btn>
          </template>
        </content-item>
        <h5 v-if="noData" class="no_data">
          {{ $t("common.nodata") }}
        </h5>
        <v-pagination
          v-model="pagination.page"
          class="my-4"
          :length="pageCount"
          color="#6166f5"
          @input="render"
          v-if="pageCount > 1"
        ></v-pagination>
      </div>
    </div>
    <popup-consume-asset
      ref="PopupConsumeAsset"
      :clinicID="clinicID"
      :episodeID="episodeID"
      :serviceID="serviceID"
      @success="
        $emit('success');
        render();
      "
    />
  </div>
</template>

<script>
import ContentItem from "@/components/cards/ContentItem.vue";
import PopupConsumeAsset from "./PopupConsumeAsset.vue";
import { convertPriceString } from "@/plugins/helpers";
import { AssetType } from "@/plugins/contants";

export default {
  components: { ContentItem, PopupConsumeAsset },
  props: ["clinicID", "episodeID", "serviceID"],
  watch: {
    clinicID() {
      this.render();
    },
  },
  data() {
    return {
      loading: false,
      keySearch: "",
      options: [],
      pagination: {
        page: 1,
        limit: 5,
      },
      pageCount: 1,
      noData: false,
    };
  },
  mounted() {
    this.render();
  },
  methods: {
    search() {
      this.noData = false;
      this.pageCount = 1;
      this.pagination = {
        page: 1,
        limit: 5,
      };
      this.render();
    },
    async render() {
      if (!this.clinicID) return;
      this.options = [];
      this.loading = true;
      var { items, error, totals } = await this.$httpClient.get(
        `/owner/clinics/${this.clinicID}/assets`,
        { keySearch: this.keySearch },
        this.pagination
      );
      this.loading = false;
      if (error) {
        return this.showError("Cannot get clinic assets!");
      }
      if (!items.length) {
        this.noData = true;
      } else {
        this.pageCount = Math.ceil(totals / this.pagination.limit);
        this.options = items.map((p) => ({
          id: p.assetID,
          title: p.assetName,
          image: false,
          content: p.price
            ? convertPriceString(p.price, true)
            : p.type == AssetType.Consumable
            ? this.$t("asset.consumable")
            : this.$t("asset.asset"),
          amount: p.amount,
        }));
      }
    },
    viewAsset(id) {
      this.$store.commit("Asset/SET_ASSET_ID", id);
    },
    clean() {
      this.noData = false;
      this.keySearch = "";
      this.pageCount = 1;
      this.pagination = {
        page: 1,
        limit: 5,
      };
    },
  },
};
</script>
