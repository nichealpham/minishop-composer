<template>
  <content-list-api :template="template" @view="view" :viewMode="viewType">
    <template v-slot:title-controller>
      <v-btn-toggle
        v-show="alias == 'patients' && isWebVersion"
        v-model="viewType"
        color="primary"
        borderless
        mandatory
        class="toggle_button_booking"
        style="border-top-left-radius: 12px"
      >
        <v-btn :value="ViewMode.Table" class="toggle_button_booking">
          <v-icon small>mdi-format-list-bulleted</v-icon>
        </v-btn>
        <v-btn :value="ViewMode.List" class="toggle_button_booking">
          <v-icon small>mdi-table-column</v-icon>
        </v-btn>
      </v-btn-toggle>
      <v-text-field
        v-show="alias != 'patients'"
        :placeholder="$t('common.search')"
        outlined
        dense
        background-color="white"
        append-icon="mdi-magnify"
        v-model="keySearch"
        v-on:keyup.enter="render({ keySearch })"
        class="mt-7 mr-2"
      ></v-text-field>
      <slot name="title-controller" />
    </template>
    <template v-slot:card-controller="{ item }">
      <v-list-item-icon class="pt-2 pb-2">
        <gender-type :gender="item.genderType" class="mr-3" />
        <v-btn
          @click="
            $event.stopPropagation();
            call(item);
          "
          small
          color="#eeeeee"
          class="service_button"
          fab
          elevation="0"
        >
          <v-icon>mdi-phone </v-icon>
        </v-btn>
      </v-list-item-icon>
    </template>
  </content-list-api>
</template>

<script>
import ContentListApi from "@/components/cards/ContentListApi";
import GenderType from "@/components/GenderType.vue";
import moment from "moment";
import { ViewMode } from "@/plugins/contants";

export default {
  props: {
    alias: {
      type: String,
      default: "patients",
    },
    viewMode: {
      type: Number,
      default: ViewMode.List,
    },
  },
  computed: {
    clinicID() {
      return this.$store.getters["Authen/getRole"].clinicID;
    },
  },
  components: { ContentListApi, GenderType },
  data: () => ({
    template: null,
    keySearch: "",
    viewType: ViewMode.List,
    ViewMode,
  }),
  mounted() {
    this.render({ keySearch: "" });
    this.viewType = this.viewMode;
  },
  watch: {
    clinicID() {
      this.render({ keySearch: "" });
    },
  },
  methods: {
    async render({ keySearch }) {
      var clinicID = this.clinicID;
      if (!clinicID) {
        return (this.template = {
          title: this.alias,
          noDataMessage:
            "You have not created any patients. Start to create one!",
        });
      }
      var paginationLimit = this.alias == "patients" ? 10 : 5;
      paginationLimit += this.viewMode == ViewMode.Table ? 2 : 0;
      this.template = {
        title: this.alias,
        api: {
          url: `/owner/clinics/${clinicID}/${this.alias}`,
          query: {
            keySearch,
          },
          headers: {
            page: 1,
            limit: paginationLimit,
          },
        },
        headers: [
          {
            text: this.$t("user.fullName"),
            // value: "title",
            value: "imageTitle",
            sortable: false,
          },
          {
            text: this.$t("user.gender"),
            value: "gender",
            sortable: false,
          },
          {
            text: this.$t("user.dob"),
            value: "dob",
            sortable: false,
          },
          {
            text: this.$t("user.profileUrl"),
            value: "profileUrl",
            sortable: false,
          },
          {
            text: "",
            value: "phone",
            sortable: false,
          },
        ],
        transform: {
          id: "userID",
          title: "fullName",
          image: {
            field: "avatar",
            transform(val) {
              return val || "icons/user.png";
            },
          },
          content: {
            field: "dob",
            transform(val) {
              return `Dob: ${moment(val).format("DD-MM-YYYY")}`;
            },
          },
        },
      };
    },
    call(item) {
      var { phone } = item;
      if (phone) this.callPhone(phone);
    },
    view(item) {
      this.$emit("view", item);
    },
  },
};
</script>
