<template>
  <div class="category_selector">
    <v-autocomplete
      chips
      deletable-chips
      multiple
      outlined
      hide-details
      dense
      :items="options"
      v-model="items"
      @keyup="handleKeyPress"
      :loading="loading"
      @change="update"
      style="padding-top: 5px"
    >
      <template v-slot:no-data>
        <v-btn
          text
          v-if="newCategoryName"
          class="text-capitalize"
          @click="createNewCategory"
          width="100%"
        >
          <v-icon class="mr-2">mdi-plus-circle-outline</v-icon>
          {{ newCategoryName }}
        </v-btn>
      </template>
    </v-autocomplete>
    <popup-confirm ref="PopupConfirm" />
  </div>
</template>

<script>
import { TargetItemType } from "@/plugins/contants";
import PopupConfirm from "./PopupConfirm.vue";

export default {
  components: { PopupConfirm },
  props: {
    targetItemID: {
      type: String,
      default: null,
    },
    targetItemType: {
      type: Number,
      default: TargetItemType.Asset,
    },
  },
  computed: {
    optionsLoaded() {
      return this.$store.getters["Asset/getCategoryOptionsLoaded"] || false;
    },
    options() {
      return this.$store.getters["Asset/getCategoryOptions"] || [];
    },
  },
  data() {
    return {
      items: [],
      newCategoryName: "",
      loading: false,
    };
  },
  created() {
    this.render();
  },
  methods: {
    handleKeyPress(e) {
      var element = document.getElementById(e.target.id);
      this.newCategoryName = element.value;
    },
    async render() {
      if (!this.$clinicID) return;
      if (!this.targetItemID || !this.targetItemType) return;
      if (!this.optionsLoaded) {
        await this.getListOptions();
      }
      this.loading = true;
      var query = { targetItemType: this.targetItemType };
      var headers = { page: 1, limit: 100 };
      var { items, error } = await this.$httpClient.get(
        `/owner/clinics/${this.$clinicID}/targetitems/${this.targetItemID}/categories`,
        query,
        headers
      );
      this.loading = false;
      if (error) {
        return this.showError(
          `Cannot get categories items! Please try again later`
        );
      }
      this.items = items.map((i) => i.categoryID);
    },
    async update() {
      var body = {
        categoryIDs: this.items,
      };
      var query = { targetItemType: this.targetItemType };
      this.loading = true;
      await this.$httpClient.put(
        `/owner/clinics/${this.$clinicID}/targetitems/${this.targetItemID}/modifycategories`,
        query,
        body
      );
      this.loading = false;
    },
    async createNewCategory() {
      if (!this.newCategoryName) return;
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      var body = {
        categoryName: this.newCategoryName,
      };
      this.loading = true;
      var { categoryID, categoryName, error } = await this.$httpClient.post(
        `/owner/clinics/${this.$clinicID}/categories`,
        {},
        body
      );
      this.loading = false;
      if (error) {
        return this.showError(
          `Cannot create new category item! Please try again later`
        );
      }
      var option = { text: categoryName, value: categoryID };
      this.newCategoryName = "";
      this.$store.commit("Asset/PUSH_CATEGORY_OPTIONS", option);
      await this.sleep(200);
      this.items.push(categoryID);
      this.update();
    },
    async getListOptions() {
      if (!this.$clinicID) return;
      this.loading = true;
      var query = { deleted: false };
      var headers = { page: 1, limit: 100 };
      var { items, error } = await this.$httpClient.get(
        `/owner/clinics/${this.$clinicID}/categories`,
        query,
        headers
      );
      this.loading = false;
      if (error) {
        return this.showError(
          `Cannot get categories items! Please try again later`
        );
      }
      var options = items.map((i) => ({
        value: i.categoryID,
        text: i.categoryName,
      }));
      this.$store.commit("Asset/SET_CATEGORY_OPTIONS", options);
      await this.sleep(200);
    },
    clean() {
      this.items = [];
      this.loading = false;
    },
  },
};
</script>

<style lang="scss">
.category_selector {
  .v-input__slot {
    padding-top: 3px !important;
  }
  .v-chip--select {
    margin-bottom: 3px !important;
  }
  .v-list-item--dense .v-list-item__title {
    margin-bottom: 0px;
  }
}
</style>
