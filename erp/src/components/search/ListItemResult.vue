<template>
  <content-list-api :template="template" @view="view">
    <template v-slot:card-controller="{ item }">
      <v-list-item-icon class="pt-2 pb-2">
        <h4 class="green--text mt-2 mr-2" v-if="isWebVersion">
          {{ convertPriceString(item.service.price) }}
        </h4>
        <v-btn
          @click="
            $event.stopPropagation();
            call(item);
          "
          small
          color="#eeeeee"
          class="service_button"
          fab
          elevation="0"
        >
          <v-icon>mdi-phone </v-icon>
        </v-btn>
      </v-list-item-icon>
    </template>
  </content-list-api>
</template>

<script>
import { convertPriceString } from "@/plugins/helpers";
import ContentListApi from "@/components/cards/ContentListApi";

export default {
  components: { ContentListApi },
  data() {
    return {
      template: null,
      optionFilter: null,
      itemDefault: null,
    };
  },
  mounted() {},
  methods: {
    convertPriceString,
    async render({ keySearch }, option) {
      this.optionFilter = option;
      switch (this.optionFilter) {
        case 1:
          this.template = {
            title: "Clinics",
            api: {
              url: `search/clinics`,
              query: {
                keySearch,
              },
              headers: {
                page: 1,
                limit: 10,
              },
            },
            transform: {
              id: {
                field: "clinic",
                transform(val) {
                  return val.clinicID;
                },
              },
              title: {
                field: "clinic",
                transform(val) {
                  return val.clinicName;
                },
              },
              content: {
                field: "service",
                transform(val) {
                  return `Service: ${val.serviceName}`;
                },
              },
              message: (() => {
                if (this.isMobileVersion) {
                  return {
                    field: "service",
                    transform: (val) => {
                      return `<b class="green--text">${this.convertPriceString(
                        val.price,
                        true
                      )}</b>`;
                    },
                  };
                }
                return {
                  field: "doctor",
                  transform(val) {
                    return `Doctor: ${val.fullName}`;
                  },
                };
              })(),
              image: {
                field: "clinic",
                transform(val) {
                  return val.logo;
                },
              },
            },
          };
          break;
        case 2:
          this.template = {
            title: "Doctors",
            api: {
              url: `search/doctors`,
              query: {
                keySearch,
              },
              headers: {
                page: 1,
                limit: 10,
              },
            },
            transform: {
              id: {
                field: "doctor",
                transform(val) {
                  return val.userID;
                },
              },
              title: {
                field: "doctor",
                transform(val) {
                  return val.fullName;
                },
              },
              content: {
                field: "service",
                transform(val) {
                  return `Service: ${val.serviceName}`;
                },
              },
              message: (() => {
                if (this.isMobileVersion) {
                  return {
                    field: "service",
                    transform: (val) => {
                      return `<b class="green--text">${this.convertPriceString(
                        val.price,
                        true
                      )}</b>`;
                    },
                  };
                }
                return {
                  field: "clinic",
                  transform(val) {
                    return `Clinic: ${val.clinicName}`;
                  },
                };
              })(),
              image: {
                field: "doctor",
                transform(val) {
                  return val.avatar || "icons/user.png";
                },
              },
            },
          };
          break;
        case 3:
          this.template = {
            title: "Services",
            api: {
              url: `search/services`,
              query: {
                keySearch,
              },
              headers: {
                page: 1,
                limit: 10,
              },
            },
            transform: {
              id: {
                field: "service",
                transform(val) {
                  return val.serviceID;
                },
              },
              title: {
                field: "service",
                transform(val) {
                  return val.serviceName;
                },
              },
              content: {
                field: "doctor",
                transform(val) {
                  return `Doctor: ${val.fullName}`;
                },
              },
              message: (() => {
                if (this.isMobileVersion) {
                  return {
                    field: "service",
                    transform: (val) => {
                      return `<b class="green--text">${this.convertPriceString(
                        val.price,
                        true
                      )}</b>`;
                    },
                  };
                }
                return {
                  field: "clinic",
                  transform(val) {
                    return `Clinic: ${val.clinicName}`;
                  },
                };
              })(),
              image: {
                field: "service",
                transform(val) {
                  return val.logo;
                },
              },
            },
          };
      }
    },
    call(item) {
      var { phone } = item.doctor || item.clinic;
      if (phone) this.callPhone(phone);
    },
    view(item) {
      this.$emit("view", item);
    },
  },
};
</script>
