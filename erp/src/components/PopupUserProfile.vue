<template>
  <v-dialog
    v-model="diaglog"
    transition="dialog-bottom-transition"
    eager
    width="500px"
    :persistent="true"
    :no-click-animation="true"
  >
    <v-card>
      <v-toolbar dark color="#6166f5">
        <v-toolbar-title class="text-capitalize">
          {{ $t("common.invite") }} {{ $t("common.user") }}
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items>
          <v-btn icon dark @click="cancel">
            <v-icon>mdi-close</v-icon>
          </v-btn>
        </v-toolbar-items>
      </v-toolbar>
      <div class="info_layout pa-5 ma-0">
        <div class="title_area">
          <div class="user__detail">
            <div class="info_layout">
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
                  :disabled="!patient.email"
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
                  @click="callPhone(patient.phone)"
                  :disabled="!patient.phone"
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
              <v-list-item class="user_divider pr-2 pl-2 mb-5">
                <v-divider></v-divider>
              </v-list-item>

              <!-- Info -->
              <div class="pl-1 pr-1">
                <p>
                  <v-icon class="mr-2" color="black"
                    >mdi-map-marker-radius-outline</v-icon
                  >
                  <b>{{ $t("user.address") }}:</b>
                  {{ patient.address }}
                </p>
                <p>
                  <v-icon class="mr-2" color="black">mdi-calendar-range</v-icon>
                  <b>{{ $t("user.dob") }}:</b>
                  {{ patient.dob }}
                  <b class="ml-5">{{ $t("user.gender") }}:</b>
                  <gender-type :gender="patient.genderType" />
                </p>
                <p>
                  <v-icon class="mr-2" color="black">
                    mdi-briefcase-outline
                  </v-icon>
                  <b>{{ $t("user.occupation") }}:</b>
                  {{ patient.occupation }}
                </p>
                <p>
                  <v-icon class="mr-2" color="black">mdi-earth</v-icon>
                  <b>{{ $t("user.country") }}:</b>
                  {{ patient.country }}
                </p>
              </div>
              <!-- End info -->
            </div>
          </div>
        </div>
      </div>
      <v-card-actions class="pb-5">
        <v-spacer></v-spacer>
        <v-btn
          class="white--text"
          color="#6166f5"
          @click="agree"
          style="width:130px"
        >
          <v-icon left dark>
            mdi-account-plus-outline
          </v-icon>
          {{ $t("common.invite") }}
        </v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import GenderType from "@/components/GenderType.vue";
import moment from "moment";
export default {
  components: { GenderType },
  props: {
    alias: {
      type: String,
      default: "Users",
    },
  },
  data() {
    return {
      diaglog: false,
      resolve: null,
      patient: {},
    };
  },
  methods: {
    async open(user) {
      if (!user) return;
      user.dob = moment(user.dob).format("DD-MM-YYYY");
      user.address = user.address || this.$t("common.notprovide");
      user.occupation = user.occupation || this.$t("common.notprovide");
      user.address = user.address || this.$t("common.notprovide");
      user.country = user.country || this.$t("common.notprovide");
      this.patient = user;

      this.diaglog = true;
      return new Promise((resolve) => {
        this.resolve = resolve;
      });
    },
    agree() {
      this.resolve(true);
      this.diaglog = false;
    },
    cancel() {
      this.resolve(false);
      this.diaglog = false;
    },
  },
};
</script>
