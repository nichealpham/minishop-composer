<template>
  <content-list :template="template" @view="view">
    <template
      v-slot:card-controller="{ item }"
      v-if="$store.getters['Authen/getRole'].roleType == 4"
    >
      <v-list-item-icon class="pt-2 pb-2">
        <v-btn
          @click="
            $event.stopPropagation();
            $emit('edit', item);
          "
          small
          color="#eeeeee"
          class="service_button"
          fab
          elevation="0"
        >
          <v-icon>mdi-square-edit-outline </v-icon>
        </v-btn>
      </v-list-item-icon>
    </template>
  </content-list>
</template>

<script>
import ContentList from "@/components/cards/ContentList.vue";
export default {
  components: { ContentList },
  props: ["clinicID"],
  data() {
    return {
      template: null,
    };
  },
  watch: {
    clinicID(id) {
      this.render(id);
    },
  },
  mounted() {
    this.render(this.clinicID);
  },
  methods: {
    async render(id) {
      if (!id) return;
      var clinic = await this.$httpClient.get(`/owner/clinics/${id}`);
      if (clinic.error) {
        return this.showError("Cannot get clinic profile!");
      }
      var { clinicID, clinicName, address, logo } = clinic;
      this.template = {
        title: "Clinic",
        items: [
          {
            id: clinicID,
            title: clinicName,
            content: address,
            image: logo || "/icons/clinic.png",
            item: clinic,
          },
        ],
      };
    },
    call(item) {
      var { phone } = item;
      if (phone) this.callPhone(phone);
    },
    view(item) {
      this.$emit("view", item);
    },
  },
};
</script>
