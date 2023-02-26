<template>
  <content-list-api :template="template" @view="view">
    <template v-slot:title-controller>
      <v-text-field
        :placeholder="$t('common.search')"
        outlined
        dense
        background-color="white"
        append-icon="mdi-magnify"
        v-model="keySearch"
        v-on:keyup.enter="render(clinicID)"
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
        <v-icon> mdi-plus </v-icon>
      </v-btn>
    </template>
    <template
      v-slot:card-controller="{ item }"
      v-if="$store.getters['Authen/isClinicStaff']"
    >
      <v-list-item-icon class="pt-2 pb-2">
        <v-icon color="green darken-2" class="mr-3" v-if="item.isSaleOrder"
          >mdi-cart-variant
        </v-icon>
        <v-btn
          @click="
            $event.stopPropagation();
            edit(item);
          "
          small
          color="#eeeeee"
          class="service_button"
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
import ContentListApi from "@/components/cards/ContentListApi.vue";
import { convertPriceString } from "@/plugins/helpers";

export default {
  components: { ContentListApi },
  props: ["clinicID"],
  data() {
    return {
      template: null,
      keySearch: "",
    };
  },
  watch: {
    clinicID(id) {
      this.render(id);
    },
  },
  mounted() {
    this.render(this.clinicID);
  },
  methods: {
    render(clinicID) {
      if (!clinicID) return;
      this.template = {
        title: "Services",
        api: {
          url: `/owner/clinics/${clinicID}/services`,
          query: {
            search: this.keySearch,
          },
          headers: {
            page: 1,
            limit: 5,
          },
        },
        transform: {
          id: "serviceID",
          title: "serviceName",
          image: "logo",
          content: {
            field: "price",
            transform(val) {
              return val ? convertPriceString(val, true) : false;
            },
          },
        },
      };
    },
    openInputService() {
      this.$emit("handleInputService");
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
