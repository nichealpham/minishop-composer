<template>
  <page-content>
    <template v-slot:toolbar>
      <v-text-field
        :placeholder="$t('common.search')"
        outlined
        dense
        background-color="white"
        append-icon="mdi-magnify"
        v-model="keySearch"
        v-on:keyup.enter="searchPatient"
      ></v-text-field>
      <v-btn color="#6166f5" class="white--text ml-3" @click="openCreate">
        <v-icon left dark> mdi-plus </v-icon>
        {{ $t("nav.patient") }}
      </v-btn>
    </template>

    <template v-slot:main>
      <list-user
        ref="ListPatient"
        class="content_card"
        :viewMode="viewMode"
        @view="openUserProfile"
      />
    </template>

    <template v-slot:side>
      <detail-user
        ref="DetailPatient"
        :full="true"
        :patientID="patientID"
        v-show="show == 1"
        @edit="openUserEdit"
        @episodeClick="handleEpisodeClick"
      />
      <div v-if="show == 5" class="btn_back_wraper" @click="show = 1">
        <v-list-item-title class="content_card_title text-center mb-3">
          <b>{{ $t("nav.episode") }}</b>
        </v-list-item-title>
        <v-btn icon>
          <v-icon>mdi-arrow-left</v-icon>
        </v-btn>
        {{ $t("common.back") }}
      </div>
      <div class="mt-5">
        <episode-detail
          ref="EpisodeDetail"
          :episodeID="episodeID"
          v-show="show == 5"
          @cancel="show = 1"
          @success="show = 1"
        />
      </div>
      <v-card outlined class="toolbox_input pa-2" v-if="patientID">
        <v-spacer />
        <v-btn
          depressed
          color="#6166f5"
          class="button white--text ml-2 text-capitalize"
          style="width: 160px"
          @click="
            $refs.ClinicInputCheckIn.clean();
            show = 4;
          "
        >
          <v-icon small class="mr-1"> mdi-logout </v-icon>
          {{ $t("common.checkin") }}
        </v-btn>
        <v-spacer />
        <v-btn
          depressed
          color="#6166f5"
          class="button white--text ml-2 text-capitalize"
          style="width: 160px"
          @click="
            $refs.ClinicCreateBooking.clean();
            show = 3;
          "
        >
          <v-icon small class="mr-1"> mdi-calendar-range </v-icon>
          {{ $t("common.appoint") }}
        </v-btn>
        <v-spacer />
      </v-card>
      <create-patient
        ref="CreatePatient"
        :patientID="patientID"
        v-show="show == 2"
        @success="onEditPatientSuccess"
        @close="show = 1"
      />
      <clinic-input-booking
        ref="ClinicCreateBooking"
        :patientID="patientID"
        v-show="show == 3"
        @success="onCreateBookingSuccess"
        @cancel="
          $refs.ClinicCreateBooking.clean();
          show = 1;
        "
      />
      <clinic-input-check-in
        ref="ClinicInputCheckIn"
        :patientID="patientID"
        v-show="show == 4"
        @cancel="
          $refs.ClinicInputCheckIn.clean();
          show = 1;
        "
      />
    </template>
  </page-content>
</template>

<script>
import PageContent from "@/components/PageContent.vue";
import ListUser from "@/components/users/ListUser";
import DetailUser from "@/components/users/DetailUser";
import CreatePatient from "@/components/users/CreatePatient.vue";
import ClinicInputBooking from "@/components/clinics/InputBooking.vue";
import ClinicInputCheckIn from "@/components/clinics/InputCheckIn.vue";
import EpisodeDetail from "@/components/episodes/EpisodeDetail.vue";
import { ViewMode } from "@/plugins/contants";
import { defaultPatientViewMode } from "@/plugins/setting";

export default {
  components: {
    PageContent,
    ListUser,
    DetailUser,
    CreatePatient,
    ClinicInputBooking,
    ClinicInputCheckIn,
    EpisodeDetail,
  },
  computed: {
    viewMode() {
      return this.isLargeWebVersion
        ? defaultPatientViewMode || ViewMode.List
        : ViewMode.List;
    },
  },
  data() {
    return {
      keySearch: "",
      show: 1,
      patientID: "",
      episodeID: "",
    };
  },
  mounted() {
    if (this.$route.query.id) {
      setTimeout(() => {
        this.openUserProfile({ id: this.$route.query.id });
      }, 1000);
    }
  },
  methods: {
    openUserProfile({ id }) {
      this.patientID = id;
      this.show = 1;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.patient")
      );
    },
    openCreate() {
      this.show = 2;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.patient")
      );
    },
    searchPatient() {
      this.$refs.ListPatient.render({ keySearch: this.keySearch });
    },
    handleEpisodeClick(episodeID) {
      this.show = 5;
      this.episodeID = episodeID;
    },
    openUserEdit({ userID }) {
      this.show = 2;
      this.$refs.CreatePatient.openEdit(userID);
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.patient")
      );
    },
    onEditPatientSuccess({ userID }) {
      this.show = 1;
      this.searchPatient();
      // this.$refs.DetailPatient.render(userID);
      this.patientID = userID;
    },
    onCreateBookingSuccess() {
      this.$refs.ClinicCreateBooking.clean();
      this.show = 1;
    },
  },
};
</script>
