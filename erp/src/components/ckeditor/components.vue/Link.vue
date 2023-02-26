<template>
  <v-dialog v-model="isShow" max-width="520px">
    <v-card>
      <v-card-title>Link</v-card-title>
      <v-card-text>
        <v-list-item class="pr-0 pl-0">
          <v-list-item-content class="pa-0">
            <v-list-item-title> <b>Name</b></v-list-item-title>
            <v-list-item-subtitle>
              <v-text-field
                dense
                outlined
                v-model="link.name"
                required
              ></v-text-field
            ></v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <v-list-item class="pr-0 pl-0">
          <v-list-item-content class="pa-0">
            <v-list-item-title> <b>Url</b></v-list-item-title>
            <v-list-item-subtitle>
              <v-text-field
                dense
                outlined
                v-model="link.url"
                required
              ></v-text-field
            ></v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-card-text>
      <v-card-actions class="pb-5">
        <v-spacer></v-spacer>
        <v-btn color="darken-1" text @click="cancel">Cancel</v-btn>
        <v-btn
          :disabled="!link.name && !link.url"
          class="pl-10 pr-10 ml-10"
          color="#6166f5"
          text
          @click="save"
        >
          Confirm
        </v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  data() {
    return {
      isShow: false,
      link: {
        name: "",
        url: "",
      },
    };
  },
  methods: {
    open() {
      this.clean();
      this.isShow = true;
    },
    save() {
      this.$emit("change", this.link);
      this.isShow = false;
    },
    cancel() {
      this.isShow = false;
    },
    clean() {
      this.link = {
        name: "",
        url: "",
      };
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
