<template>
  <v-dialog v-model="showDialog" max-width="600px">
    <v-card class="pa-0 ma-0">
      <v-card-title>
        {{ $t("asset.consumeRequest") }}: {{ asset.assetName }}
      </v-card-title>
      <v-form v-model="valid" @submit.prevent="consumeID ? update() : create()">
        <v-card-text class="pa-0 ma-0 pl-5 pr-5">
          <b>{{ $t("asset.consumeAmmount") }}</b>
          <v-text-field dense outlined v-model="body.amount"></v-text-field>
          <b>{{
            asset.type == AssetType.Consumable
              ? $t("asset.consumePriceEach")
              : $t("asset.consumePrice")
          }}</b>
          <v-text-field dense outlined v-model="body.price"></v-text-field>
          <b>{{ $t("asset.consumeDescription") }}</b>
          <v-textarea
            rows="3"
            v-model="body.description"
            dense
            outlined
          ></v-textarea>
        </v-card-text>
        <v-card-actions class="pb-5">
          <v-spacer></v-spacer>
          <v-btn color="darken-1" text @click="close">{{
            $t("common.cancel")
          }}</v-btn>
          <v-btn
            class="pl-10 pr-10 ml-10 white--text"
            color="#6166f5"
            :loading="loading"
            type="submit"
            :disabled="!valid"
            >{{ $t("common.confirmed") }}</v-btn
          >
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script>
import { AssetType } from "@/plugins/contants";

export default {
  props: ["clinicID", "episodeID", "serviceID"],
  data() {
    return {
      AssetType,
      valid: false,
      loading: false,
      showDialog: false,
      body: {
        amount: 1,
        price: null,
        description: "",
      },
      consumeID: null,
      assetID: null,
      asset: {},
    };
  },
  created() {},
  methods: {
    openCreate(assetID) {
      this.clean();
      this.assetID = assetID;
      this.showDialog = true;
      this.loadAsset();
    },
    openEdit(consumeID) {
      this.clean();
      this.consumeID = consumeID;
      this.showDialog = true;
      this.loadConsume();
    },
    async loadAsset() {
      if (!this.assetID) return;
      this.loading = true;
      var result = await this.$httpClient.get(`/owner/assets/${this.assetID}`);
      this.loading = false;
      if (result.error) return;
      if (result.type == AssetType.Consumable) this.body.price = result.price;
      this.asset = result;
    },
    async loadConsume() {
      if (!this.consumeID) return;
      if (!this.clinicID || !this.episodeID || !this.serviceID) return;
      this.loading = true;
      var result = await this.$httpClient.get(
        `/owner/clinics/${this.clinicID}/episodes/${this.episodeID}/assetconsumes/${this.consumeID}`
      );
      this.loading = false;
      if (result.error) return;
      var { amount, price, description, assetID, asset } = result;
      this.assetID = assetID;
      this.body = {
        amount,
        price,
        description,
      };
      this.asset = asset;
    },
    async create() {
      if (!this.clinicID || !this.episodeID || !this.assetID || !this.serviceID)
        return;
      this.loading = true;
      var body = {
        ...this.body,
        assetID: this.assetID,
        serviceID: this.serviceID,
        episodeID: this.episodeID,
      };
      var result = await this.$httpClient.post(
        `/owner/clinics/${this.clinicID}/episodes/${this.episodeID}/assetconsumes`,
        null,
        body
      );
      this.loading = false;
      if (result.error) return;
      this.$emit("success");
      this.close();
    },
    async update() {
      if (!this.consumeID) return;
      if (!this.clinicID || !this.episodeID || !this.assetID || !this.serviceID)
        return;
      this.loading = true;
      var result = await this.$httpClient.put(
        `/owner/clinics/${this.clinicID}/episodes/${this.episodeID}/assetconsumes/${this.consumeID}`,
        null,
        this.body
      );
      this.loading = false;
      if (result.error) return;
      this.$emit("success");
      this.close();
    },
    clean() {
      this.loading = false;
      this.consumeID = null;
      this.assetID = null;
      this.body = {
        amount: 1,
        price: null,
        description: "",
      };
      this.asset = {};
    },
    close() {
      this.clean();
      this.showDialog = false;
    },
  },
};
</script>
