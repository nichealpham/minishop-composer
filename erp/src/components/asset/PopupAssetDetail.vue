<template>
  <v-dialog v-model="showDialog" max-width="700px" eager>
    <v-card>
      <div class="info_layout pa-5">
        <div class="title_area">
          <div class="user__detail mb-10">
            <asset-detail ref="AssetDetail" />
          </div>
        </div>
      </div>
    </v-card>
  </v-dialog>
</template>

<script>
import AssetDetail from "./AssetDetail.vue";

export default {
  components: { AssetDetail },
  computed: {
    assetID() {
      return this.$store.getters["Asset/getAssetID"] || "";
    },
  },
  watch: {
    assetID(val) {
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
      if (this.assetID) {
        this.showDialog = true;
        this.$refs.AssetDetail.render(this.assetID);
      }
    },
    clean() {
      this.showDialog = false;
      this.$store.commit("Asset/SET_ASSET_ID", null);
    },
  },
};
</script>
