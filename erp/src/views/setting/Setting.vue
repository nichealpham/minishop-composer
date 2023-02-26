<template>
  <page-content>
    <template v-slot:toolbar>
      <v-select
        :items="clinics"
        v-model="clinicID"
        label="Clinic"
        solo
        class="select"
        dense
      ></v-select>
      <v-btn color="#6166f5" class="white--text ml-3" @click="openCreateClinic">
        <v-icon left dark> mdi-plus </v-icon>
        {{ $t("nav.clinic") }}
      </v-btn>
    </template>

    <template v-slot:main>
      <clinic-profile
        ref="ClinicProfile"
        :clinicID="clinicID"
        class="content_card"
        @view="openClinicProfile"
        @edit="openEditClinic"
      />
      <list-service
        ref="ListService"
        class="content_card"
        :clinicID="clinicID"
        @create="openCreateService"
        @view="openServiceProfile"
        @edit="openEditService"
      />
      <list-user
        ref="ListDoctors"
        :clinicID="clinicID"
        class="content_card"
        alias="doctors"
        @view="openDetailDoctor"
      >
        <template
          v-if="$store.getters['Authen/getRole'].roleType > 1"
          v-slot:title-controller
        >
          <v-btn
            x-small
            fab
            @click="openCreateDoctor"
            color="#6166f5"
            class="white--text"
            elevation="0"
          >
            <v-icon> mdi-plus </v-icon>
          </v-btn>
        </template>
      </list-user>
      <list-asset
        ref="ListAssets"
        :clinicID="clinicID"
        class="content_card"
        @create="openCreateAsset"
        @edit="openEditAsset"
        @view="openViewAsset"
      />
    </template>

    <template v-slot:side>
      <detail-clinic
        ref="ClinicDetail"
        :clinicID="clinicID"
        :full="true"
        v-show="show == 1"
      />
      <input-clinic
        ref="ClinicEdit"
        v-show="show == 2"
        @close="closeClinicInput"
        @success="createClinicSuccess"
      />
      <input-service
        :clinicID="clinicID"
        ref="ServiceEdit"
        v-show="show == 3"
        @close="closeServiceInput"
        @success="createServiceSuccess"
      />
      <detail-service :full="true" ref="ServiceDetail" v-show="show == 4" />
      <detail-user
        :full="false"
        :patientID="doctorID"
        ref="UserDetail"
        v-show="show == 5"
        @edit="onDoctorEdit"
      />
      <create-patient
        ref="CreatePatient"
        alias="doctors"
        :patientID="doctorID"
        v-show="show == 6"
        @success="onDoctorSuccess"
        @close="show = 1"
      />
      <asset-input
        ref="AssetInput"
        v-show="show == 7"
        @close="show = 1"
        @success="onAssetSuccess"
        @delete="
          $refs.ListAssets.render();
          show = 1;
        "
      />
      <asset-detail ref="AssetDetail" v-show="show == 8" />
    </template>
  </page-content>
</template>

<script>
import ClinicProfile from "@/components/clinics/ClinicProfile";
import ListService from "@/components/services/ListService.vue";
import PageContent from "@/components/PageContent.vue";
import DetailClinic from "@/components/clinics/DetailClinic";
import InputService from "@/components/services/InputService";
import InputClinic from "@/components/clinics/InputClinic";
import DetailService from "@/components/services/DetailService.vue";
import ListUser from "@/components/users/ListUser.vue";
import DetailUser from "@/components/users/DetailUser.vue";
import CreatePatient from "@/components/users/CreatePatient.vue";
import ListAsset from "@/components/asset/ListAsset.vue";
import AssetInput from "@/components/asset/AssetInput.vue";
import AssetDetail from "@/components/asset/AssetDetail.vue";

export default {
  components: {
    ListService,
    PageContent,
    ClinicProfile,
    DetailClinic,
    InputClinic,
    InputService,
    DetailService,
    ListUser,
    DetailUser,
    CreatePatient,
    ListAsset,
    AssetInput,
    AssetDetail,
  },
  computed: {
    clinicID: {
      get() {
        return this.$store.getters["Authen/getRole"].clinicID.toLowerCase();
      },
      set(val) {
        this.$store.commit("Authen/SET_ROLE", val);
      },
    },
  },
  data() {
    return {
      show: 1,
      clinics: [],
      doctorID: "",
    };
  },
  mounted() {
    this.renderClinics();
  },
  methods: {
    async renderClinics() {
      var items = this.$store.getters["Authen/getUser"].roles || [];
      this.clinics = items.map((r) => ({
        text: r.clinicName,
        value: r.clinicID.toLowerCase(),
      }));
      if (!this.clinicID && this.clinics.length) {
        this.clinicID = this.clinics[0].value;
      }
    },
    async createClinicSuccess({ clinicID }) {
      await this.renderClinics();
      this.$refs.ClinicProfile.render(clinicID);
      this.$refs.ClinicDetail.render(clinicID);
    },
    async createServiceSuccess({ serviceID }) {
      this.$refs.ListService.render(this.clinicID);
      this.openServiceProfile({ id: serviceID });
    },
    closeClinicInput() {
      this.show = 1;
    },
    closeServiceInput() {
      this.show = 1;
    },
    openEditClinic() {
      this.show = 2;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.clinic")
      );
      this.$refs.ClinicEdit.openEdit(this.clinicID);
    },
    openEditAsset({ id }) {
      this.show = 7;
      this.openDiaglogIfMobile(this.$t("cardTitle.assets"));
      this.$refs.AssetInput.openEdit(id);
    },
    openEditService({ id }) {
      this.show = 3;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.service")
      );
      this.$refs.ServiceEdit.openEdit(id);
    },
    openClinicProfile() {
      this.show = 1;
      this.$refs.ClinicDetail.hideCover();
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.clinic")
      );
    },
    openViewAsset({ id }) {
      this.show = 8;
      this.openDiaglogIfMobile(this.$t("cardTitle.assets"));
      this.$refs.AssetDetail.render(id);
    },
    openServiceProfile({ id }) {
      this.show = 4;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.service")
      );
      this.$refs.ServiceDetail.render(id);
    },
    openCreateClinic() {
      this.show = 2;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.clinic")
      );
    },
    openCreateAsset() {
      this.show = 7;
      this.openDiaglogIfMobile(this.$t("asset.create"));
      this.$refs.AssetInput.clean();
    },
    openCreateService() {
      this.show = 3;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.service")
      );
      this.$refs.ServiceEdit.clean();
    },
    openCreateDoctor() {
      this.show = 6;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.doctor")
      );
    },
    onDoctorEdit({ userID }) {
      this.show = 6;
      this.$refs.CreatePatient.openEdit(userID);
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.doctor")
      );
    },
    openDetailDoctor({ id }) {
      this.show = 5;
      this.doctorID = id;
      this.openDiaglogIfMobile(
        this.$t("common.info") + " " + this.$t("nav.doctor")
      );
    },
    onDoctorSuccess({ userID }) {
      this.show = 5;
      this.doctorID = userID;
      this.$refs.UserDetail.render(userID);
      this.$refs.ListDoctors.render({ keySearch: "" });
    },
    onAssetSuccess({ assetID }) {
      this.$refs.ListAssets.render();
      this.show = 8;
      this.openDiaglogIfMobile(this.$t("cardTitle.assets"));
      this.$refs.AssetDetail.render(assetID);
    },
  },
};
</script>
