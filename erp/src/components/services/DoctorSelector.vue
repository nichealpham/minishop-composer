<template>
  <div>
    <v-list-item-title class="content_card_title text-center mb-3">
      <v-btn
        @click="$emit('back')"
        fab
        small
        elevation="0"
        style="position: absolute; top: -8px; left: 10px"
      >
        <v-icon> mdi-arrow-left </v-icon>
      </v-btn>
      <b>
        {{ $t("common.choose") + " " + $t("nav.doctor") }}
      </b>
    </v-list-item-title>
    <div class="pl-1 pr-1">
      <v-text-field
        v-model="keySearch"
        :label="$t('common.search') + ' ' + $t('nav.doctor')"
        outlined
        full-width
        dense
        hide-details
        background-color="white"
        append-icon="mdi-magnify"
        class="mt-0 pt-0 mb-4"
        v-on:keyup.enter="search"
      />
      <div style="text-align: center" v-if="loading">
        <v-progress-circular
          indeterminate
          color="primary"
        ></v-progress-circular>
      </div>
      <div v-else>
        <content-item
          v-for="(item, index) in results"
          :key="index"
          :item="item"
          @view="selectDoctor"
        >
          <template v-slot:controller>
            <v-btn fab small elevation="0">
              <v-icon> mdi-arrow-right </v-icon>
            </v-btn>
          </template>
        </content-item>
      </div>
    </div>
  </div>
</template>

<script>
import { convertPriceString, standardizing } from "@/plugins/helpers";
import ContentItem from "@/components/cards/ContentItem.vue";

export default {
  components: { ContentItem },
  computed: {
    clinicID() {
      return this.$store.getters["Authen/getRole"].clinicID;
    },
  },
  props: {
    serviceID: {
      type: String,
      default: "",
    },
    value: {
      type: Object,
      default: function () {
        return {
          userID: "",
          fullName: "",
        };
      },
    },
  },
  watch: {
    clinicID() {
      this.render();
    },
    serviceID() {
      this.render();
    },
  },
  data() {
    return {
      loading: false,
      keySearch: "",
      doctors: [],
      results: [],
    };
  },
  mounted() {},
  methods: {
    async render() {
      if (!this.serviceID) return;
      this.loading = true;
      var service = await this.$httpClient.get(
        `/owner/services/${this.serviceID}`
      );
      this.loading = false;
      if (service.error) {
        return this.showError("Cannot get service info!");
      }
      this.doctors = (service.listProviders || []).map((p) => ({
        id: p.userID,
        title: p.fullName,
        image: p.avatar || "icons/user.png",
        content: p.specialPrice
          ? convertPriceString(p.specialPrice, true)
          : service.price
          ? convertPriceString(service.price, true)
          : false,
      }));
      this.results = this.doctors;
    },
    search() {
      this.results = this.doctors.filter((d) =>
        standardizing(d.title).includes(standardizing(this.keySearch))
      );
    },
    selectDoctor({ id, title }) {
      this.$emit("input", { userID: id, fullName: title });
      this.$emit("done");
    },
    convertPriceString,
  },
};
</script>
