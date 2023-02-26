<template>
  <div>
    <v-list-item-title class="content_card_title text-center mb-3">
      <b>{{ $t("common.choose") }} {{ $t("nav.doctor") }}</b>
    </v-list-item-title>
    <div class="pl-1 pr-1">
      <v-text-field
        v-model="keySearch"
        :placeholder="$t('common.search') + ' ' + $t('nav.doctor')"
        outlined
        full-width
        dense
        hide-details
        background-color="white"
        append-icon="mdi-magnify"
        class="mt-0 pt-0 mb-4"
        v-on:keyup.enter="getDoctors"
      />
      <div style="text-align: center" v-if="loading">
        <v-progress-circular
          indeterminate
          color="primary"
        ></v-progress-circular>
      </div>
      <div v-else>
        <content-item
          v-for="(item, index) in options"
          :key="index"
          :item="item"
          @view="selectDoctor"
        >
          <template v-slot:controller>
            <v-btn fab small elevation="0">
              <v-icon
                :style="{
                  opacity: item.checked ? '1' : '0',
                }"
              >
                mdi-check
              </v-icon>
            </v-btn>
          </template>
        </content-item>
      </div>
    </div>
    <v-card outlined class="toolbox_input pa-2">
      <v-spacer />
      <v-btn depressed color="#eeeeee" class="button" @click="cancel">
        <v-icon small class="mr-2"> mdi-close-circle-outline </v-icon>
        {{ $t("common.cancel") }}
      </v-btn>
      <v-spacer />
      <v-btn
        type="submit"
        depressed
        color="#6166f5"
        class="button white--text ml-2"
        @click="save"
      >
        <v-icon class="mr-2" small>mdi-content-save-outline</v-icon>
        {{ $t("common.save") }}
      </v-btn>
      <v-spacer />
    </v-card>
  </div>
</template>

<script>
import ContentItem from "@/components/cards/ContentItem.vue";

export default {
  components: { ContentItem },
  computed: {
    clinicID() {
      return this.$store.getters["Authen/getRole"].clinicID;
    },
  },
  props: {
    value: {
      type: Array,
      default: function () {
        return [
          // {
          //   id: "",
          //   userID: "",
          //   title: "",
          //   image: "",
          // },
        ];
      },
    },
  },
  watch: {
    clinicID() {
      this.getDoctors();
    },
  },
  data() {
    return {
      loading: false,
      keySearch: "",
      doctors: [],
      options: [],
    };
  },
  mounted() {
    this.getDoctors();
  },
  methods: {
    async getDoctors() {
      if (!this.clinicID) return;
      this.loading = true;
      var { items, error, statusCode } = await this.$httpClient.get(
        `/owner/clinics/${this.clinicID}/doctors`,
        { keySearch: this.keySearch },
        { page: 1, limit: 100 }
      );
      this.loading = false;
      if (error) {
        if (statusCode == "404") return;
        return this.showError("Cannot get doctors info!");
      }
      this.doctors = items;
      this.render();
    },
    render() {
      this.options = this.doctors.map((p) => ({
        id: p.userID,
        title: p.fullName,
        image: p.avatar || "icons/user.png",
        checked: this.value.find((i) => i.userID == p.userID),
      }));
    },
    selectDoctor({ id, title, image }) {
      var ind = this.value.findIndex((i) => i.userID == id);
      if (ind == -1) {
        this.value.push({
          id,
          userID: id,
          title,
          image,
        });
      } else {
        this.value.splice(ind, 1);
      }
      this.render();
    },
    save() {
      this.$emit("input", this.value);
      this.$emit("done");
      this.clean();
    },
    cancel() {
      this.$emit("close");
      this.clean();
    },
    clean() {
      this.keySearch = "";
      this.render();
    },
  },
};
</script>
