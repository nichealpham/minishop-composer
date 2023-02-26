<template>
  <v-dialog v-model="showDialog" max-width="600px">
    <v-card class="pa-0 ma-0">
      <v-card-title>
        {{ $t("asset.importInventory") }}
      </v-card-title>
      <v-form v-model="valid" @submit.prevent="save">
        <v-card-text class="pa-0 ma-0 pl-5 pr-5">
          <b>{{ $t("asset.amountChange") }}</b>
          <v-text-field dense outlined v-model="body.amount"></v-text-field>
          <b>{{ $t("asset.reasonChange") }}</b>
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
export default {
  computed: {
    clinicID() {
      return (
        this.$store.getters["Authen/getRole"].clinicID || ""
      ).toLowerCase();
    },
  },
  data() {
    return {
      valid: false,
      loading: false,
      showDialog: false,
      assetID: null,
      body: {
        amount: "",
        description: "",
      },
    };
  },
  created() {},
  methods: {
    open(assetID) {
      this.assetID = assetID;
      this.showDialog = true;
    },
    async save() {
      if (!this.assetID || !this.clinicID) return;
      this.loading = true;
      var result = await this.$httpClient.post(
        `/owner/clinics/${this.clinicID}/assets/${this.assetID}/import`,
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
      this.assetID = null;
      this.body = {
        amount: "",
        description: "",
      };
    },
    close() {
      this.clean();
      this.showDialog = false;
    },
  },
};
</script>
