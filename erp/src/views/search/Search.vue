<template>
  <page-content>
    <template v-slot:toolbar>
      <v-text-field
        :placeholder="$t('common.search') + '...'"
        outlined
        dense
        background-color="white"
        append-icon="mdi-magnify"
        class="mt-0 pt-0"
        v-model="keySearch"
        v-on:keyup.enter="searchItem"
        ref="SearchText"
      ></v-text-field>
      <v-select
        v-model="selection"
        :items="options"
        item-text="text"
        item-value="value"
        background-color="white"
        outlined
        dense
        class="ml-2"
        style="width:120px"
      ></v-select>
    </template>

    <template v-slot:main>
      <list-item-result
        class="content_card"
        ref="ListItemResult"
        @view="openSearchResult"
      />
    </template>

    <template v-slot:side>
      <div class="search_detail">
        <v-expansion-panels v-model="pannel" multiple flat v-show="show == 1">
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
              <detail-service ref="DetailService" />
            </v-expansion-panel-content>
          </v-expansion-panel>

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
              <detail-user ref="DetailUser" />
            </v-expansion-panel-content>
          </v-expansion-panel>

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
              <detail-clinic ref="DetailClinic" />
            </v-expansion-panel-content>
          </v-expansion-panel>
          <v-card outlined class="toolbox_input pa-2">
            <v-spacer />
            <v-btn
              depressed
              color="#6166f5"
              class="button white--text ml-2 text-capitalize"
              @click="show = 2"
            >
              <v-icon class="mr-2" small>mdi-calendar-month-outline</v-icon>
              {{ $t("common.appoint") }}
            </v-btn>
            <v-spacer />
          </v-card>
        </v-expansion-panels>
      </div>

      <patient-input-booking
        ref="PatientInputBooking"
        v-show="show == 2"
        @success="createBookingSuccess"
        @cancel="show = 1"
      />
    </template>
  </page-content>
</template>

<script>
import PageContent from "@/components/PageContent.vue";
import ListItemResult from "@/components/search/ListItemResult";
import DetailClinic from "@/components/clinics/DetailClinic";
import DetailService from "@/components/services/DetailService";
import DetailUser from "@/components/users/DetailUser";
import PatientInputBooking from "@/components/booking/PatientInputBooking.vue";

export default {
  components: {
    PageContent,
    ListItemResult,
    DetailClinic,
    DetailService,
    DetailUser,
    PatientInputBooking,
  },
  data() {
    return {
      show: 1,
      pannel: [0, 1, 2],
      selection: 3,
      keySearch: "",
      options: [
        {
          value: 1,
          text: this.$t("nav.clinic"),
        },
        {
          value: 2,
          text: this.$t("nav.doctor"),
        },
        {
          value: 3,
          text: this.$t("nav.service"),
        },
      ],
    };
  },
  watch: {
    selection(option) {
      this.$refs.ListItemResult.render({ keySearch: this.keySearch }, option);
    },
  },
  mounted() {
    this.$refs.SearchText.focus();
    this.getCurrentOption();
  },
  methods: {
    createBookingSuccess({ appointmentID }) {
      this.$router.push(`/booking?id=${appointmentID}`);
    },
    searchItem() {
      this.$refs.ListItemResult.render(
        { keySearch: this.keySearch },
        this.selection
      );
    },
    getCurrentOption() {
      this.$refs.ListItemResult.render(
        { keySearch: this.keySearch },
        this.selection
      );
    },
    openSearchResult(item) {
      this.$refs.DetailClinic.render(item.clinicID);
      this.$refs.DetailUser.render(item.userID);
      this.$refs.DetailService.render(item.serviceID);
      this.$refs.PatientInputBooking.render(item);
      this.pannel = [0, 1, 2];
      this.openDiaglogIfMobile(this.$t("common.detail"));
    },
  },
};
</script>

<style lang="scss">
.search_detail {
  .v-expansion-panel {
    border: 1px soild #bebebe;
    .v-expansion-panel-header {
      b {
        text-align: center;
        text-transform: uppercase;
      }
      i {
        margin-top: -4px;
      }
    }
  }
}
</style>
