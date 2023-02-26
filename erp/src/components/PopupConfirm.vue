<template>
  <v-dialog v-model="isShow" max-width="420px">
    <v-card>
      <v-card-title v-html="title"></v-card-title>
      <v-card-text v-html="message"></v-card-text>
      <v-card-actions class="pb-5">
        <v-spacer></v-spacer>
        <v-btn color="darken-1" text @click="cancel">{{
          $t("common.cancel")
        }}</v-btn>
        <v-btn class="pl-10 pr-10 ml-10" color="#6166f5" text @click="agree">{{
          $t("common.confirmed")
        }}</v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    title: {
      type: String,
      default: function() {
        return this.$t("asset.confirm");
        // return "Confirm";
      },
    },
    message: {
      type: String,
      default: function() {
        return this.$t("asset.confirmMessage");
        // return "Are you sure";
      },
    },
  },
  data() {
    return {
      isShow: false,
      resolve: null,
    };
  },
  methods: {
    confirm() {
      this.isShow = true;
      return new Promise((resolve) => {
        this.resolve = resolve;
      });
    },
    agree() {
      this.resolve(true);
      this.isShow = false;
    },
    cancel() {
      this.resolve(false);
      this.isShow = false;
    },
  },
};
</script>

<style lang="scss">
.v-dialog {
  .v-card__text {
    font-size: 1rem;
  }
}
</style>
