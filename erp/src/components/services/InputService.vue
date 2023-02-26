<template>
  <div>
    <v-form
      v-show="step == 1"
      v-model="valid"
      @submit.prevent="serviceID ? update() : create()"
    >
      <v-list-item-title class="content_card_title text-center mb-3">
        <b>{{ $t("common.detail") }}</b>
      </v-list-item-title>
      <v-row class="avatar__detail mt-2">
        <v-avatar size="120px">
          <img alt="Avatar" :src="body.logo || 'icons/service.png'" />
        </v-avatar>
        <choose-file v-model="body.logo" />
      </v-row>
      <v-list>
        <v-list-item class="pr-0 pl-0">
          <v-list-item-content class="pa-0">
            <v-list-item-title>
              <b>{{ $t("service.name") }}</b></v-list-item-title
            >
            <v-list-item-subtitle>
              <v-text-field
                placeholder="Input Service Name"
                dense
                outlined
                v-model="body.serviceName"
                required
                :rules="textRules"
              ></v-text-field>
              <v-checkbox
                v-model="body.isSaleOrder"
                :label="$t('common.isSaleOrder')"
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
              <b>{{ $t("service.price") }}</b></v-list-item-title
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
              <b>{{ $t("service.description") }}</b></v-list-item-title
            >
          </v-list-item-content>
        </v-list-item>
        <text-editor v-model="body.shortDescription" :showButtons="false" />

        <v-list-item class="pr-0 pl-0">
          <v-list-item-content class="pa-0">
            <v-list-item-title>
              <b>{{ $t("nav.doctor") }}</b></v-list-item-title
            >
            <v-list-item-subtitle
              v-if="!body.providers || !body.providers.length"
              class="sub_text_detail"
            >
              {{ $t("service.noproviders") }}
            </v-list-item-subtitle>
          </v-list-item-content>
          <v-list-item-icon>
            <v-btn
              color="#6166f5"
              class="white--text"
              @click="
                $refs.DoctorSelector.render();
                step = 2;
              "
              small
              elevation="0"
              fab
            >
              <v-icon>
                mdi-plus
              </v-icon>
            </v-btn>
          </v-list-item-icon>
        </v-list-item>
        <content-item
          v-for="(item, index) in body.providers"
          :key="index"
          :item="item"
        >
          <template v-slot:controller>
            <v-btn fab small elevation="0" @click="removeDoctor(item)">
              <v-icon>
                mdi-close
              </v-icon>
            </v-btn>
          </template>
        </content-item>
      </v-list>
      <v-card outlined class="toolbox_input pa-2">
        <v-spacer />
        <v-btn depressed color="#eeeeee" class="button" @click="close">
          <v-icon small class="mr-2">
            mdi-close-circle-outline
          </v-icon>
          {{ $t("common.cancel") }}
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

    <doctor-selector
      ref="DoctorSelector"
      v-model="body.providers"
      v-show="step == 2"
      @done="step = 1"
      @close="step = 1"
    />
  </div>
</template>

<script>
import DoctorSelector from "@/components/clinics/DoctorSelector.vue";
import ContentItem from "@/components/cards/ContentItem.vue";
import ChooseFile from "@/components/chooseFile.vue";
import { mapGetters } from "vuex";
import TextEditor from "@/components/ckeditor/TextEditor.vue";

export default {
  props: ["clinicID"],
  components: {
    ContentItem,
    ChooseFile,
    DoctorSelector,
    TextEditor,
  },
  computed: {
    ...mapGetters({
      user: "Authen/getUser",
    }),
  },
  data() {
    return {
      step: 1,
      valid: false,
      serviceID: null,
      loading: false,
      body: {
        clinicID: this.clinicID,
        serviceName: "",
        shortDescription: "",
        logo: "icons/service.png",
        price: "",
        isSaleOrder: false,
        providers: [],
      },
      textRules: [
        (v) => !!v || "Value is required",
        (v) => v.length >= 4 || "Content must be bigger than 4 characters",
      ],
    };
  },
  mounted() {
    this.clean();
  },
  methods: {
    async openEdit(serviceID) {
      if (!serviceID) return;
      this.clean();
      this.serviceID = serviceID;
      this.loading = true;
      var data = await this.$httpClient.get(
        `/owner/clinics/${this.clinicID}/services/${serviceID}`
      );
      if (data.error) {
        this.showError(`Register clinic failed! Please try again later`);
      } else {
        data.providers = data.listProviders.map((p) => ({
          id: p.userID,
          userID: p.userID,
          title: p.fullName,
          image: p.avatar || "icons/user.png",
        }));
        this.body = data;
      }
      this.loading = false;
    },
    async update() {
      if (!this.serviceID) return;
      var body = this.prepareBody();
      this.loading = true;
      var data = await this.$httpClient.put(
        `/owner/clinics/${this.clinicID}/services/${this.serviceID}`,
        null,
        body
      );
      if (data.error) {
        this.showError(`Update service failed! Please try again later`);
      } else {
        this.$emit("success", data);
        this.clean();
      }
      this.loading = false;
    },
    async create() {
      this.loading = true;
      var body = this.prepareBody();
      var data = await this.$httpClient.post(
        `/owner/clinics/${this.clinicID}/services`,
        null,
        body
      );
      if (data.error) {
        this.showError(`Register service failed! Please try again later`);
      } else {
        this.$emit("success", data);
        this.clean();
      }
      this.loading = false;
    },
    prepareBody() {
      if (!this.body.providers || !this.body.providers.length) {
        return this.showError("Must add atleast 1 doctor!");
      }
      var body = { ...this.body };
      body.price = !body.price || body.price == "FREE" ? 0 : body.price;
      return body;
    },
    removeDoctor({ userID }) {
      var ind = this.body.providers.findIndex((p) => p.userID == userID);
      if (ind != -1) {
        this.body.providers.splice(ind, 1);
      }
    },
    clean() {
      this.step = 1;
      this.serviceID = null;
      this.body = {
        clinicID: this.clinicID,
        serviceName: "",
        shortDescription: "",
        logo: "icons/service.png",
        price: "",
        isSaleOrder: false,
        providers: [
          {
            id: this.user.userID,
            userID: this.user.userID,
            title: this.user.fullName,
            image: this.user.avatar || "icons/user.png",
            content: false,
          },
        ],
      };
    },
    close() {
      this.clean();
      this.closeDiaglogIfMobile();
      this.$emit("close");
    },
  },
};
</script>
