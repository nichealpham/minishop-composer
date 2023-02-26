<template>
  <v-dialog v-model="showDialog" max-width="700px" eager>
    <v-card>
      <div class="info_layout pa-5">
        <div class="title_area">
          <div class="user__detail mb-10">
            <detail-service ref="DetailService" :full="true" />
          </div>
        </div>
      </div>
    </v-card>
  </v-dialog>
</template>

<script>
import DetailService from "./DetailService.vue";
export default {
  components: { DetailService },
  computed: {
    serviceID() {
      return this.$store.getters["Asset/getServiceID"] || "";
    },
  },
  watch: {
    serviceID(val) {
      if (val) {
        this.render();
      }
    },
    showDialog(val) {
      if (!val) {
        this.clean();
      }
    },
  },
  data() {
    return {
      showDialog: false,
    };
  },
  created() {},
  methods: {
    async render() {
      if (this.serviceID) {
        this.showDialog = true;
        this.$refs.DetailService.render(this.serviceID);
      }
    },
    clean() {
      this.showDialog = false;
      this.$store.commit("Asset/SET_SERVICE_ID", null);
    },
  },
};
</script>
