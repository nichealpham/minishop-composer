<template>
  <div>
    <div class="pl-1 pr-1 mt-1 d-flex">
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
        v-if="editable || pageCount > 1"
      />
      <v-btn
        depressed
        color="#6166f5"
        class="button white--text text-capitalize ml-2"
        style="width:105px;margin-top:2px;"
        @click="$emit('add')"
        v-if="editable"
      >
        <v-icon class="mr-1" small>mdi-plus</v-icon>
        {{ $t("asset.addItem") }}
      </v-btn>
    </div>
    <div style="text-align:center" v-if="loading">
      <v-progress-circular indeterminate color="#6166f5"></v-progress-circular>
    </div>
    <div v-else>
      <div v-if="totalPrice">
        <h3
          class="text-center text-capitalize green--text"
          style="margin-top:-8px;"
        >
          {{ $t("common.totalPrice") }}
        </h3>
        <h2 class="mb-1 green--text text-center">
          {{ formatPrice(totalPrice) }}
        </h2>
      </div>

      <content-item
        v-for="(item, index) in options"
        :key="index"
        :item="item"
        @view="viewAsset(item.aid)"
      >
        <template v-slot:controller>
          <h4 class="mr-3">
            <span style="font-weight:400;" v-if="!isMobileVersion"
              >{{ $t("asset.consumeAmmountShort") }}:</span
            >
            {{ item.amount }}
          </h4>

          <v-btn
            fab
            small
            elevation="0"
            @click="
              $event.stopPropagation();
              $refs.PopupConsumeAsset.openEdit(item.id);
            "
            class="mr-3"
            v-if="editable"
          >
            <v-icon>
              mdi-square-edit-outline
            </v-icon>
          </v-btn>

          <v-btn
            fab
            small
            elevation="0"
            @click="
              $event.stopPropagation();
              remove(item.id);
            "
            :loading="loading"
            v-if="editable"
          >
            <v-icon>
              mdi-close
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
    <popup-confirm ref="PopupConfirm" />
  </div>
</template>

<script>
import ContentItem from "@/components/cards/ContentItem.vue";
import PopupConsumeAsset from "./PopupConsumeAsset.vue";
import { convertPriceString } from "@/plugins/helpers";
import moment from "moment";
import PopupConfirm from "@/components/PopupConfirm.vue";

export default {
  components: { ContentItem, PopupConsumeAsset, PopupConfirm },
  props: ["clinicID", "episodeID", "serviceID", "editable"],
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
      totalPrice: 0,
    };
  },
  mounted() {
    this.search();
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
      if (!this.clinicID || !this.episodeID || !this.serviceID) return;
      this.options = [];
      this.loading = true;
      var { items, error, totals } = await this.$httpClient.get(
        `/owner/clinics/${this.clinicID}/episodes/${this.episodeID}/assetconsumes`,
        { keySearch: this.keySearch },
        this.pagination
      );
      this.loading = false;
      if (error) {
        return this.showError("Cannot get attached assets!");
      }
      if (!items.length) {
        this.noData = true;
      } else {
        this.calculateTotalPrice(items);
        this.pageCount = Math.ceil(totals / this.pagination.limit);
        this.options = items.map((p) => {
          var content = `<span style="font-size:0.7rem;">${moment(
            p.dateCreated
          ).format(
            "HH:mm, DD-MM-YYYY"
          )}<span class="green--text ml-2" style="font-size:0.75rem;"><b>${
            p.price ? convertPriceString(p.price, true) : ""
          }</b></span></span>`;
          var message = p.description
            ? `<span style="font-size:0.85rem;">${p.description}</span>`
            : false;
          return {
            id: p.assetConsumedID,
            aid: p.assetID,
            title: `<b>${p.asset.assetName}</b>`,
            image: false,
            content,
            message,
            amount: p.amount,
          };
        });
      }
    },
    async remove(consumeID) {
      if (!this.clinicID || !this.episodeID || !this.serviceID) return;
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      this.loading = true;
      var result = await this.$httpClient.delete(
        `/owner/clinics/${this.clinicID}/episodes/${this.episodeID}/assetconsumes/${consumeID}`
      );
      this.loading = false;
      if (!result || result.error) return;
      this.$emit("delete");
      this.render();
    },
    viewAsset(id) {
      this.$store.commit("Asset/SET_ASSET_ID", id);
    },
    calculateTotalPrice(items) {
      function add(accumulator, a) {
        return accumulator + a;
      }
      var prices = items.map((o) => o.price * o.amount);
      this.totalPrice = prices.reduce(add, 0);
    },
    formatPrice(val) {
      return convertPriceString(val, true);
    },
    clean() {
      this.totalPrice = 0;
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
