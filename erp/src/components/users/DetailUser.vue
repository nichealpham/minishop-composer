<template>
  <book-cover ref="BookCover">
    <v-btn
      icon
      @click="render(patientID)"
      fixed
      top
      right
      color="white"
      style="right: 70px; top: -45px; position: absolute"
    >
      <v-icon>mdi-refresh</v-icon>
    </v-btn>
    <v-list-item-title class="content_card_title text-center mb-3">
      <b>{{ $t("common.detail") }}</b>
    </v-list-item-title>
    <v-btn
      v-if="
        patient.statusID == 2 && $store.getters['Authen/getRole'].roleType == 4
      "
      @click="edit"
      elevation="2"
      fab
      bottom
      right
      fixed
      color="#6166f5"
      class="white--text mb-12"
    >
      <v-icon>mdi-square-edit-outline </v-icon>
    </v-btn>
    <!-- <v-btn
      v-if="
        isWebVersion &&
        patient.statusID == 2 &&
        $store.getters['Authen/getRole'].roleType == 4
      "
      @click="edit"
      top
      right
      fixed
      icon
      color="white"
      style="top: 10px"
    >
      <v-icon>mdi-square-edit-outline </v-icon>
    </v-btn> -->
    <div style="text-align: center" v-if="loading">
      <v-progress-circular indeterminate color="#6166f5"></v-progress-circular>
    </div>
    <div class="info_layout" v-else>
      <v-row class="avatar__detail mt-2">
        <v-avatar size="100px">
          <img alt="Avatar" :src="patient.avatar || 'icons/user.png'" />
        </v-avatar>
      </v-row>
      <v-row class="name__detail">
        <b class="mb-1">{{ patient.fullName }}</b>
      </v-row>
      <v-row class="mb-2 user_button_detail">
        <v-btn
          depressed
          color="#eeeeee"
          class="button mr-2 text-capitalize"
          style="width: 100px"
          @click="openHolterVn(patient.phone)"
        >
          HolterVN
        </v-btn>
        <v-btn
          v-if="patient.email"
          depressed
          color="#eeeeee"
          class="button mr-2 text-capitalize"
          style="width: 100px"
        >
          <v-icon left dark> mdi-email-edit-outline </v-icon>
          {{ $t("user.email") }}
        </v-btn>
        <!-- <v-btn
          v-if="patient.profileUrl"
          @click="openUrl(patient.profileUrl)"
          depressed
          color="#eeeeee"
          class="button mr-2 text-capitalize"
          style="width: 100px"
        >
          <v-icon left dark> mdi-email-edit-outline </v-icon>
          {{ patient.profileUrl }}
        </v-btn> -->
        <v-btn
          @click="callPhone(patient.phone)"
          depressed
          color="#eeeeee"
          class="button text-capitalize"
          style="width: 100px"
        >
          <v-icon left dark> mdi-phone </v-icon>
          {{ $t("user.call") }}
        </v-btn>
      </v-row>
      <v-list-item class="user_divider pr-2 pl-2 mb-5">
        <v-divider></v-divider>
      </v-list-item>
      <!-- Info -->
      <div class="pl-1 pr-1">
        <p>
          <v-icon class="mr-2" color="black"
            >mdi-card-account-details-outline</v-icon
          >
          <b>{{ $t("user.profileUrl") }}:</b>
          {{ patient.profileUrl }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-calendar-range</v-icon>
          <b>{{ $t("user.dob") }}:</b>
          {{ patient.dob }}
          <b class="ml-5">{{ $t("user.gender") }}:</b>
          <gender-type :gender="patient.genderType" />
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-earth</v-icon>
          <b>{{ $t("user.address") }}:</b>
          {{ patient.address }}
        </p>
        <p>
          <v-icon class="mr-2" color="black"> mdi-briefcase-outline </v-icon>
          <b>{{ $t("user.occupation") }}:</b>
          {{ patient.occupation }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-flag-outline</v-icon>
          <b>{{ $t("user.country") }}:</b>
          {{ patient.country }}
        </p>
        <p v-if="full">
          <v-icon class="mr-2" color="black"> mdi-history </v-icon>
          <b>{{ $t("common.lastvisits") }}: </b>
        </p>
      </div>
      <!-- End info -->
    </div>
    <div class="pl-1 pr-1" v-if="full">
      <content-list-api
        :loadingColor="'#6166f5'"
        :template="template"
        @view="view"
      >
      </content-list-api>
    </div>
  </book-cover>
</template>

<script>
import ContentListApi from "@/components/cards/ContentListApi.vue";
import GenderType from "@/components/GenderType.vue";
import BookCover from "@/components/BookCover.vue";
import moment from "moment";
export default {
  components: { ContentListApi, GenderType, BookCover },
  props: {
    patientID: {
      type: String,
      default: function () {
        return "";
      },
    },
    full: {
      type: Boolean,
      default: false,
    },
  },
  computed: {
    clinicID() {
      return this.$store.getters["Authen/getRole"].clinicID || "";
    },
  },
  data() {
    return {
      patient: {
        avatar: "",
        fullName: "Patient Name",
        genderType: false,
        dob: "---",
        occupation: "---",
        country: "---",
        address: "---",
        email: "",
        phone: "",
      },
      loading: false,
      template: {},
    };
  },
  watch: {
    patientID(val) {
      this.render(val);
    },
  },
  mounted() {
    this.render(this.patientID);
  },
  methods: {
    openUrl(url) {
      window.open(url, "blank");
    },
    openHolterVn(phone) {
      if (!phone) return;
      phone = this.getPhoneLastDigits(phone);
      this.$store.commit("MiniApp/OPEN_APP", {
        name: "HolterVN",
        config: { url: `https://huyetap.phongkham.co/public/0${phone}` },
      });
    },
    async render(patientID) {
      if (!patientID || !this.clinicID) return;
      this.$refs.BookCover.showContent();
      this.renderLastVisits(patientID);

      this.loading = true;
      var patient = await this.$httpClient.get(
        `/owner/clinics/${this.clinicID}/patients/${patientID}`
      );
      if (patient.error) {
        this.showError("Cannot get patient profile! Please try again later");
      } else {
        patient.dob = moment(patient.dob).format("DD-MM-YYYY");
        patient.address = patient.address || this.$t("common.notprovide");
        patient.occupation = patient.occupation || this.$t("common.notprovide");
        patient.address = patient.address || this.$t("common.notprovide");
        patient.country = patient.country || this.$t("common.notprovide");
        this.patient = patient;
      }
      this.loading = false;
    },
    async renderLastVisits(patientID) {
      if (!patientID || !this.clinicID) return;
      this.template = {
        title: "",
        api: {
          url: `/owner/clinics/${this.clinicID}/patients/${patientID}/lastvisits`,
          query: {
            keySearch: "",
          },
          headers: {
            page: 1,
            limit: 3,
          },
        },
        transform: {
          id: "episodeID",
          image: {
            field: "clinicLogo",
            transform(clinicLogo) {
              return clinicLogo || "/icons/clinic.png";
            },
          },
          title: {
            field: "records",
            transform: (records) => {
              var serviceName = records && records[0] && records[0].serviceName;
              var doctorName =
                records && records[0] && records[0].userAppointName;
              return `
              <b>${serviceName}</b>
              <br/>
              <span style="font-size:14px;">${this.$t(
                "nav.doctor"
              )}: ${doctorName}</span>`;
            },
          },
          // content: {
          //   field: "clinicName",
          //   transform(clinicName) {
          //     return `<span style="font-size:14px">${clinicName}</span>`;
          //   },
          // },
          content: "",
          message: {
            field: "dateCreated",
            transform(dateCreated) {
              return `<span style="font-size:13px">${moment(dateCreated).format(
                "HH:mm - DD/MM/YYYY"
              )}</span>`;
            },
          },
        },
      };
    },
    edit() {
      this.closeDiaglogIfMobile();
      this.$emit("edit", this.patient);
    },
    view(item) {
      this.$emit("episodeClick", item.id);
    },
  },
};
</script>
