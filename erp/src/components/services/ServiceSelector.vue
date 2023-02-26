<template>
  <div>
    <v-list-item-title class="content_card_title text-center mb-3">
      <b>{{ $t("common.choose") }} {{ $t("nav.service") }}</b>
    </v-list-item-title>
    <div class="pl-1 pr-1">
      <v-text-field
        v-model="keySearch"
        :label="$t('common.search') + ' ' + $t('nav.service')"
        outlined
        full-width
        dense
        hide-details
        background-color="white"
        append-icon="mdi-magnify"
        class="mt-0 pt-0 mb-2"
        v-on:keyup.enter="render"
      />
      <content-list-api
        :template="template"
        @view="selectService"
        @click="$emit('click')"
      >
        <template v-slot:card-controller>
          <v-list-item-icon class="pt-2 pb-2">
            <v-btn
              small
              color="#eeeeee"
              class="service_button"
              fab
              elevation="0"
            >
              <v-icon>mdi-arrow-right </v-icon>
            </v-btn>
          </v-list-item-icon>
        </template>
      </content-list-api>
    </div>
  </div>
</template>

<script>
import { convertPriceString } from "@/plugins/helpers";
import ContentListApi from "@/components/cards/ContentListApi.vue";

export default {
  components: { ContentListApi },
  props: {
    value: {
      type: Object,
      default: function () {
        return {
          serviceID: "",
          serviceName: "",
          price: "",
        };
      },
    },
  },
  computed: {
    clinicID() {
      return this.$store.getters["Authen/getRole"].clinicID;
    },
  },
  watch: {
    clinicID() {
      this.render();
    },
  },
  data() {
    return {
      keySearch: "",
      template: {},
    };
  },
  mounted() {
    this.template = {
      api: {
        url: `/owner/clinics/${this.clinicID}/services`,
        query: {
          search: this.keySearch,
        },
        headers: {
          page: 1,
          limit: 10,
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
  methods: {
    async render() {
      this.template = {
        ...this.template,
        api: {
          url: `/owner/clinics/${this.clinicID}/services`,
          query: {
            search: this.keySearch,
          },
          headers: {
            page: 1,
            limit: 10,
          },
        },
      };
    },
    selectService({ id, title }) {
      this.$emit("input", { serviceID: id, serviceName: title });
      this.$emit("done");
    },
    convertPriceString,
  },
};
</script>
