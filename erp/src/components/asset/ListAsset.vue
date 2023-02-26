<template>
  <content-list-api :template="template" @view="view">
    <template v-slot:title-controller>
      <popup-import-asset ref="PopupImportAsset" @success="render" />
      <v-text-field
        :placeholder="$t('common.search')"
        outlined
        dense
        background-color="white"
        append-icon="mdi-magnify"
        v-model="keySearch"
        v-on:keyup.enter="render()"
        class="mt-7"
      ></v-text-field>
      <v-btn
        v-if="$store.getters['Authen/isClinicStaff']"
        x-small
        fab
        @click="create"
        color="#6166f5"
        class="white--text ml-2"
        elevation="0"
      >
        <v-icon>
          mdi-plus
        </v-icon>
      </v-btn>
    </template>
    <template
      v-slot:card-controller="{ item }"
      v-if="$store.getters['Authen/isClinicStaff']"
    >
      <v-list-item-icon class="pt-2 pb-2">
        <h4 class="mt-2 mr-3" v-if="item.type == AssetType.Consumable">
          <span style="font-weight:400;" v-if="!isMobileVersion"
            >{{ $t("asset.instock") }}:</span
          >
          <span style="font-size:1.1rem;" class="ml-2">{{ item.amount }}</span>
        </h4>
        <v-btn
          @click="
            $event.stopPropagation();
            $refs.PopupImportAsset.open(item.id);
          "
          v-if="item.type == AssetType.Consumable"
          small
          color="#eeeeee"
          class="service_button "
          fab
          elevation="0"
        >
          <v-icon>mdi-plus-minus-variant </v-icon>
        </v-btn>
        <v-btn
          @click="
            $event.stopPropagation();
            edit(item);
          "
          small
          color="#eeeeee"
          class="service_button ml-3"
          fab
          elevation="0"
        >
          <v-icon>mdi-square-edit-outline </v-icon>
        </v-btn>
      </v-list-item-icon>
    </template>
  </content-list-api>
</template>

<script>
import { convertPriceString } from "@/plugins/helpers";
import ContentListApi from "@/components/cards/ContentListApi.vue";
import PopupImportAsset from "./PopupImportAsset.vue";
import { AssetType } from "@/plugins/contants";

export default {
  components: { ContentListApi, PopupImportAsset },
  computed: {
    clinicID() {
      return (
        this.$store.getters["Authen/getRole"].clinicID || ""
      ).toLowerCase();
    },
  },
  data() {
    return {
      template: null,
      keySearch: "",
      AssetType,
    };
  },
  watch: {
    clinicID() {
      this.render();
    },
  },
  mounted() {
    this.render();
  },
  methods: {
    render() {
      if (!this.clinicID) return;
      this.template = {
        title: "Assets",
        api: {
          url: `/owner/clinics/${this.clinicID}/assets`,
          query: {
            keySearch: this.keySearch,
          },
          headers: {
            page: 1,
            limit: 5,
          },
        },
        transform: {
          id: "assetID",
          title: "assetName",
          image: false,
          content: {
            field: "price",
            transform(val) {
              return val ? convertPriceString(val, true) : false;
            },
          },
        },
      };
    },
    view(item) {
      this.$emit("view", item);
    },
    edit(item) {
      this.$emit("edit", item);
    },
    remove(item) {
      this.$emit("remove", item);
    },
    create() {
      this.$emit("create");
    },
  },
};
</script>
