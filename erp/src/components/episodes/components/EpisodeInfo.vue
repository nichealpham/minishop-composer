<template>
  <div
    class="info_layout mt-2"
    v-if="
      episode &&
        episode.userAdmitted &&
        episode.clinic &&
        record &&
        record.service &&
        record.userAppoint
    "
  >
    <div style="text-align:center" class="mt-5 mb-1" v-if="status.message">
      <v-row class="avatar__detail mt-1">
        <v-avatar>
          <img alt="Avatar" :src="status.image" />
        </v-avatar>
      </v-row>
      <h3
        class="text-center text-capitalize"
        :class="{
          'green--text': status.success,
          'red--text': !status.success,
        }"
        style="margin-top:-8px;"
      >
        {{ status.message }}
      </h3>
      <h2
        class="mb-1 green--text"
        v-if="episode.statusID == 2 && !showEditPrice"
      >
        {{ formatPrice(episode.totalPrice) }}
        <v-btn
          v-if="!$isPatient"
          @click="
            showEditPrice = true;
            editedPrice = episode.totalPrice;
          "
          icon
          class="button"
          color="green"
          style="margin-top:-5px;"
          large
        >
          <v-icon>mdi-square-edit-outline</v-icon>
        </v-btn>
      </h2>
      <div
        v-if="showEditPrice && !$isPatient"
        class="mt-2"
        style="height:auto;position:relative;float:left;width:100%;"
      >
        <v-text-field
          placeholder="Price"
          dense
          filled
          v-model="editedPrice"
          style="width:160px;float:left;margin-left:calc(50% - 145px)"
        ></v-text-field>
        <v-btn
          depressed
          color="green"
          class="button white--text text-capitalize"
          style="float:left;margin:2px 0 0 10px;"
          @click="updateInvoice"
          :loading="loading"
        >
          <v-icon class="mr-1" small>mdi-check-circle-outline</v-icon>
          {{ $t("common.save") }}
        </v-btn>
        <v-btn
          @click="showEditPrice = false"
          icon
          class="button"
          style="float:left;margin:-2px 0 0 5px;"
          large
        >
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </div>
    </div>
    <div class="w-100 text-center" v-if="!$isPatient">
      <episode-public-share :episode="episode" />
    </div>
    <v-expansion-panels v-model="pannel" multiple flat>
      <v-expansion-panel>
        <v-expansion-panel-header class="pa-0 ma-0">
          <b>
            <v-icon class="mr-2" color="black">
              mdi-package-variant-closed
            </v-icon>
            {{ $t("cardTitle.assets") }}
          </b>
        </v-expansion-panel-header>
        <v-expansion-panel-content class="pa-0 ma-0">
          <asset-attached
            ref="AssetAttached"
            :clinicID="episode.clinicID"
            :episodeID="episode.episodeID"
            :serviceID="record.service && record.service.serviceID"
            :editable="
              episode.statusID == EpisodeStatusType.CheckedIn && !$isPatient
            "
          />
        </v-expansion-panel-content>
      </v-expansion-panel>

      <!-- Service -->
      <v-expansion-panel>
        <v-expansion-panel-header class="pa-0 ma-0">
          <b>
            <v-icon class="mr-2" color="black">
              mdi-clipboard-file-outline
            </v-icon>
            {{ $t("nav.service") }}
          </b>
        </v-expansion-panel-header>
        <v-expansion-panel-content class="pa-0 ma-0">
          <v-list-item-title class="content_card_title text-center mb-3">
            <b>Service Info</b>
          </v-list-item-title>
          <v-row class="avatar__detail mt-1">
            <v-avatar size="100px">
              <img
                alt="Avatar"
                :src="record.service.logo || 'icons/service.png'"
              />
            </v-avatar>
          </v-row>
          <h3 class="text-center">{{ record.service.serviceName }}</h3>
          <v-row class="name__detail mt-1 green--text">
            <h3 class="mb-1">{{ formatPrice(record.service.price) }}</h3>
          </v-row>
          <v-list-item class="pr-2 pl-2 mb-5 mt-5">
            <v-divider></v-divider>
          </v-list-item>
          <div class="pl-3 pr-3">
            <div>
              <v-icon class="mr-2" color="black"
                >mdi-clipboard-text-outline</v-icon
              >
              <b>{{ $t("service.description") }}:</b>
              <div
                v-html="record.service.shortDescription"
                class="mt-3 mb-3 editor_result_content"
              ></div>
            </div>
          </div>
        </v-expansion-panel-content>
      </v-expansion-panel>
      <!-- End Service -->

      <!-- Patient -->
      <v-expansion-panel>
        <v-expansion-panel-header class="pa-0 ma-0">
          <b>
            <v-icon class="mr-2" color="black">
              mdi-shield-account-outline
            </v-icon>
            {{ $t("nav.patient") }}
          </b>
        </v-expansion-panel-header>
        <v-expansion-panel-content class="pa-0 ma-0">
          <!-- Avatar -->
          <v-row class="avatar__detail mt-1">
            <v-avatar size="100px">
              <img
                alt="Avatar"
                :src="episode.userAdmitted.avatar || 'icons/user.png'"
              />
            </v-avatar>
          </v-row>
          <h3 class="text-center">{{ episode.userAdmitted.fullName }}</h3>
          <v-row class="mb-2 user_button_detail">
            <v-btn
              :disabled="!episode.userAdmitted.email"
              depressed
              color="#eeeeee"
              class="button mr-2 text-capitalize"
              style="width:100px"
            >
              <v-icon left dark>
                mdi-email-edit-outline
              </v-icon>
              {{ $t("user.email") }}
            </v-btn>
            <v-btn
              @click="callPhone(episode.userAdmitted.phone)"
              :disabled="!episode.userAdmitted.phone"
              depressed
              color="#eeeeee"
              class="button text-capitalize"
              style="width:100px"
            >
              <v-icon left dark>
                mdi-phone
              </v-icon>
              {{ $t("user.call") }}
            </v-btn>
          </v-row>
          <v-list-item class="pr-2 pl-2 mb-5 mt-5">
            <v-divider></v-divider>
          </v-list-item>
          <!-- End Avatar -->
          <div class="pl-1 pr-1">
            <p>
              <v-icon class="mr-2" color="black">mdi-calendar-range</v-icon>
              <b>{{ $t("user.dob") }}:</b>
              {{ formatDateTime(episode.userAdmitted.dob) }}
              <b class="ml-5">{{ $t("user.gender") }}:</b>
              <gender-type :gender="episode.userAdmitted.genderType" />
            </p>
            <p>
              <v-icon class="mr-2" color="black">mdi-alarm-check</v-icon>
              <b>{{ $t("episode.checkintime") }}:</b>
              <b class="green--text ml-1">{{
                formatTimeBooking(episode.timeStart)
              }}</b>
            </p>
          </div>
          <div class="pl-1 pr-1"></div>
        </v-expansion-panel-content>
      </v-expansion-panel>
      <!-- End Patient -->

      <!-- Doctor -->
      <v-expansion-panel>
        <v-expansion-panel-header class="pa-0 ma-0">
          <b>
            <v-icon class="mr-2" color="black">
              mdi-account-box-outline
            </v-icon>
            {{ $t("nav.doctor") }}
          </b>
        </v-expansion-panel-header>
        <v-expansion-panel-content class="pa-0 ma-0">
          <!-- Avatar -->
          <v-row class="avatar__detail mt-1">
            <v-avatar size="100px">
              <img
                alt="Avatar"
                :src="record.userAppoint.avatar || 'icons/user.png'"
              />
            </v-avatar>
          </v-row>
          <h3 class="text-center">{{ record.userAppoint.fullName }}</h3>
          <v-row class="mb-2 user_button_detail">
            <v-btn
              :disabled="!record.userAppoint.email"
              depressed
              color="#eeeeee"
              class="button mr-2 text-capitalize"
              style="width:100px"
            >
              <v-icon left dark>
                mdi-email-edit-outline
              </v-icon>
              {{ $t("user.email") }}
            </v-btn>
            <v-btn
              @click="callPhone(record.userAppoint.phone)"
              :disabled="!record.userAppoint.phone"
              depressed
              color="#eeeeee"
              class="button text-capitalize"
              style="width:100px"
            >
              <v-icon left dark>
                mdi-phone
              </v-icon>
              {{ $t("user.call") }}
            </v-btn>
          </v-row>
          <v-list-item class="pr-2 pl-2 mb-5 mt-5">
            <v-divider></v-divider>
          </v-list-item>
          <!-- End Avatar -->
          <div class="pl-1 pr-1">
            <p>
              <v-icon class="mr-2" color="black">mdi-calendar-range</v-icon>
              <b>{{ $t("user.dob") }}:</b>
              {{ formatDateTime(record.userAppoint.dob) }}
              <b class="ml-5">{{ $t("user.gender") }}:</b>
              <gender-type :gender="record.userAppoint.genderType" />
            </p>
          </div>
          <div class="pl-1 pr-1"></div>
        </v-expansion-panel-content>
      </v-expansion-panel>
      <!-- End Doctor -->

      <!-- Clinic -->
      <v-expansion-panel>
        <v-expansion-panel-header class="pa-0 ma-0">
          <b>
            <v-icon class="mr-2" color="black">
              mdi-home-plus-outline
            </v-icon>
            {{ $t("nav.clinic") }}
          </b>
        </v-expansion-panel-header>
        <v-expansion-panel-content class="pa-0 ma-0">
          <v-list-item-title class="content_card_title text-center mb-3">
            <b>{{ $t("common.info") + " " + $t("nav.clinic") }}</b>
          </v-list-item-title>
          <div class="info_layout">
            <v-row class="avatar__detail mt-1">
              <v-avatar size="100px">
                <img
                  alt="Avatar"
                  :src="episode.clinic.logo || 'icons/clinic.png'"
                />
              </v-avatar>
            </v-row>
            <v-row class="name__detail">
              <b class="mb-1">{{ episode.clinic.clinicName }}</b>
            </v-row>
            <v-list-item class="pr-2 pl-2 mb-5 mt-5">
              <v-divider></v-divider>
            </v-list-item>
            <div class="pl-1 pr-1">
              <p>
                <v-icon class="mr-2" color="black">
                  mdi-map-marker-radius-outline
                </v-icon>
                <b>{{ $t("user.address") }}:</b>
                {{ episode.clinic.address }}
              </p>
              <p>
                <v-icon class="mr-2" color="black"
                  >mdi-clipboard-text-outline</v-icon
                >
                <b>{{ $t("clinic.description") }}:</b>
                {{ episode.clinic.shortDescription }}
              </p>
              <p>
                <v-icon class="mr-2" color="black"
                  >mdi-file-code-outline</v-icon
                >
                <b>{{ $t("clinic.tax") }}:</b>
                {{ episode.clinic.taxNumber || $t("common.notprovide") }}
              </p>
            </div>
          </div>
        </v-expansion-panel-content>
      </v-expansion-panel>
      <!-- End Clinic -->
    </v-expansion-panels>
  </div>
</template>

<script>
import { convertPriceString } from "@/plugins/helpers";
import { EpisodeStatusType } from "@/plugins/contants";
import moment from "moment";
import GenderType from "@/components/GenderType.vue";
import AssetAttached from "./AssetAttached.vue";
import EpisodePublicShare from "./EpisodePublicShare.vue";

export default {
  components: { GenderType, AssetAttached, EpisodePublicShare },
  props: ["episode"],
  watch: {
    episode: {
      deep: true,
      handler() {
        this.render();
      },
    },
  },
  data() {
    return {
      EpisodeStatusType,
      pannel: [0],
      record: {},
      status: {
        image: "",
        message: "",
        success: true,
      },
      showEditPrice: false,
      editedPrice: null,
      loading: false,
    };
  },
  mounted() {
    this.render();
  },
  methods: {
    render() {
      if (!this.episode) return;
      this.clean();
      var episode = this.episode;
      this.record = (episode.records && episode.records[0]) || {};
      var { statusID } = episode;
      if (statusID == EpisodeStatusType.Success) {
        this.status = {
          image: "icons/tick.png",
          message: this.$t("episode.success"),
          success: true,
        };
      }
      if (statusID == EpisodeStatusType.Canceled) {
        this.status = {
          image: "icons/close.png",
          message: this.$t("episode.canceled"),
          success: false,
        };
      }
    },
    async updateInvoice() {
      var body = {
        price: this.editedPrice,
      };
      var clinicID = this.episode.clinicID;
      var episodeID = this.episode.episodeID;
      this.loading = true;
      var result = await this.$httpClient.put(
        `/owner/clinics/${clinicID}/episodes/${episodeID}/invoice`,
        {},
        body
      );
      this.loading = false;
      if (result) {
        this.episode.totalPrice = this.editedPrice;
        this.showEditPrice = false;
      }
    },
    formatPrice(val) {
      return convertPriceString(val, true);
    },
    formatTimeBooking(val) {
      return moment(val).format("HH:mm , DD-MM-YYYY");
    },
    formatDateTime(val) {
      return moment(val).format("DD-MM-YYYY");
    },
    clean() {
      this.pannel = [0];
      this.status = {
        image: "",
        message: "",
        success: true,
      };
      this.showEditPrice = false;
      this.editedPrice = null;
    },
  },
};
</script>
