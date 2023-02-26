<template>
  <div>
    <v-form
      v-show="step == 1"
      v-model="valid"
      @submit.prevent="assetID ? update() : create()"
    >
      <v-list-item-title class="content_card_title text-center mb-3">
        <b>{{ $t("cardTitle.assets") }}</b>
      </v-list-item-title>

      <v-list>
        <v-list-item class="pr-0 pl-0">
          <v-list-item-content class="pa-0">
            <v-list-item-title>
              <b>{{ $t("asset.assetName") }}</b>
            </v-list-item-title>
            <v-list-item-subtitle>
              <v-text-field
                placeholder="Input name"
                dense
                outlined
                v-model="body.assetName"
                required
                :rules="textRules"
                class="ma-0 pa-0"
              ></v-text-field>
              <v-checkbox
                v-model="body.isSale"
                :label="$t('common.forSale')"
                style="color:black;margin-top:-10px !important"
                hide-details
                class="pa-0 ma-0"
              ></v-checkbox>
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-list-item class="pr-0 pl-0 mt-3">
          <v-list-item-content class="pa-0">
            <v-list-item-title>
              <b>{{ $t("common.saleHighlight") }}</b>
            </v-list-item-title>
            <v-list-item-subtitle>
              <v-textarea rows="2" v-model="body.saleDescription" outlined />
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-list-item class="pr-0 pl-0" style="margin-top:-10px;">
          <v-list-item-content class="pa-0">
            <v-list-item-title>
              <b>{{ $t("asset.description") }}</b>
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <text-editor v-model="body.description" :showButtons="false" />
      </v-list>

      <v-expansion-panels multiple flat>
        <v-expansion-panel>
          <v-expansion-panel-header class="pa-0 ma-0">
            <b>{{ $t("asset.advancesetting") }}</b>
          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <v-list>
              <v-list-item class="pr-0 pl-0">
                <v-list-item-content class="pa-0">
                  <v-list-item-title>
                    <b>{{ $t("asset.type") }}</b></v-list-item-title
                  >
                  <v-list-item-subtitle>
                    <v-select
                      :disabled="!!assetID"
                      dense
                      outlined
                      v-model="body.type"
                      :items="typeOptions"
                    ></v-select>
                  </v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>

              <v-list-item
                class="pr-0 pl-0"
                v-if="body.type == AssetType.Consumable"
              >
                <v-list-item-content class="pa-0">
                  <v-list-item-title>
                    <b>{{ $t("asset.amount") }}</b></v-list-item-title
                  >
                  <v-list-item-subtitle>
                    <v-text-field
                      :disabled="!!assetID"
                      placeholder="Input amount"
                      dense
                      outlined
                      v-model="body.amount"
                    ></v-text-field
                  ></v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>

              <v-list-item class="pr-0 pl-0">
                <v-list-item-content class="pa-0">
                  <v-list-item-title>
                    <b>{{ $t("asset.price") }}</b></v-list-item-title
                  >
                  <v-list-item-subtitle>
                    <v-text-field
                      placeholder="Input Price"
                      dense
                      outlined
                      v-model="body.price"
                    ></v-text-field
                  ></v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>

              <v-list-item class="pr-0 pl-0">
                <v-list-item-content class="pa-0">
                  <v-list-item-title>
                    <b>{{ $t("asset.assetCode") }}</b></v-list-item-title
                  >
                  <v-list-item-subtitle>
                    <v-text-field
                      placeholder="Input code"
                      dense
                      outlined
                      v-model="body.assetCode"
                    ></v-text-field
                  ></v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>

      <v-card outlined class="toolbox_input pa-2">
        <v-spacer />
        <v-btn
          depressed
          :color="assetID ? 'red' : '#eeeeee'"
          class="button"
          :class="{ 'white--text': assetID }"
          @click="closeOrDelete"
        >
          <v-icon small class="mr-2">
            mdi-close-circle-outline
          </v-icon>
          {{ $t("common.delete") }}
        </v-btn>
        <v-spacer />
        <v-btn
          type="submit"
          depressed
          color="#6166f5"
          class="button white--text ml-2"
          :loading="loading"
          :disabled="!valid"
        >
          <v-icon class="mr-2" small>mdi-content-save-outline</v-icon>
          {{ $t("common.save") }}
        </v-btn>
        <v-spacer />
      </v-card>
    </v-form>
    <popup-confirm
      ref="PopupConfirm"
      :title="confirm.title"
      :message="confirm.message"
    />
  </div>
</template>

<script>
import { AssetType } from "@/plugins/contants";
import TextEditor from "@/components/ckeditor/TextEditor.vue";
import PopupConfirm from "@/components/PopupConfirm.vue";

export default {
  components: {
    TextEditor,
    PopupConfirm,
  },
  computed: {
    clinicID() {
      return (
        this.$store.getters["Authen/getRole"].clinicID || ""
      ).toLowerCase();
    },
    typeOptions() {
      return [
        { text: this.$t("asset.asset"), value: AssetType.Asset },
        { text: this.$t("asset.consumable"), value: AssetType.Consumable },
      ];
    },
  },
  data() {
    return {
      AssetType,
      step: 1,
      valid: false,
      assetID: null,
      loading: false,
      body: {
        assetName: "",
        assetCode: "",
        description: "",
        type: AssetType.Consumable,
        amount: 1,
        price: null,
        isSale: false,
        saleDescription: null,
      },
      textRules: [
        (v) => !!v || "Value is required",
        (v) => v.length >= 4 || "Content must be bigger than 4 characters",
      ],
      deleting: false,
      confirm: {
        title: this.$t("asset.create"),
        message: this.$t("asset.createAssetConfirmMessage"),
      },
    };
  },
  mounted() {
    this.clean();
  },
  methods: {
    async openEdit(assetID) {
      if (!assetID) return;
      this.clean();
      this.assetID = assetID;
      this.loading = true;
      var data = await this.$httpClient.get(
        `/owner/clinics/${this.clinicID}/assets/${assetID}`
      );
      if (data.error) {
        this.showError(`Register asset failed! Please try again later`);
      } else {
        this.body = data;
      }
      this.loading = false;
    },
    async update() {
      if (!this.assetID) return;
      var body = this.prepareBody();
      this.loading = true;
      var data = await this.$httpClient.put(
        `/owner/clinics/${this.clinicID}/assets/${this.assetID}`,
        null,
        body
      );
      if (data.error) {
        this.showError(`Update asset failed! Please try again later`);
      } else {
        this.$emit("success", data);
        this.clean();
      }
      this.loading = false;
    },
    async create() {
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      this.loading = true;
      var body = this.prepareBody();
      var data = await this.$httpClient.post(
        `/owner/clinics/${this.clinicID}/assets`,
        null,
        body
      );
      if (data.error) {
        this.showError(`Register asset failed! Please try again later`);
      } else {
        this.$emit("success", data);
        this.clean();
      }
      this.loading = false;
    },
    prepareBody() {
      var body = { ...this.body };
      body.price = !body.price || body.price == "FREE" ? null : body.price;
      return body;
    },
    clean() {
      this.step = 1;
      this.assetID = null;
      this.body = {
        assetName: "",
        assetCode: "",
        description: "",
        type: AssetType.Consumable,
        amount: 1,
        price: null,
        isSale: false,
        saleDescription: null,
      };
      this.confirm = {
        title: this.$t("asset.create"),
        message: this.$t("asset.createAssetConfirmMessage"),
      };
    },
    async closeOrDelete() {
      if (!this.assetID) return this.close();
      this.confirm = {
        title: this.$t("asset.delete"),
        message: this.$t("asset.deleteAssetConfirmMessage"),
      };
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      this.deleting = true;
      await this.$httpClient.delete(
        `/owner/clinics/${this.clinicID}/assets/${this.assetID}`
      );
      this.deleting = false;
      this.clean();
      this.closeDiaglogIfMobile();
      this.$emit("delete");
    },
    close() {
      this.clean();
      this.closeDiaglogIfMobile();
      this.$emit("close");
    },
  },
};
</script>

<style lang="scss">
.v-list-item .v-list-item__title,
.v-list-item .v-list-item__subtitle {
  margin-bottom: 5px;
}
</style>
