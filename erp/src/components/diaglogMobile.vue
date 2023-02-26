<template>
  <v-dialog
    v-model="diaglog"
    fullscreen
    hide-overlay
    transition="dialog-bottom-transition"
    eager
    :persistent="true"
    :no-click-animation="true"
  >
    <v-card style="overflow-x:hidden;">
      <v-toolbar dark color="#6166f5">
        <v-toolbar-title>
          {{ title }}
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items>
          <v-btn icon dark @click="close">
            <v-icon>mdi-close</v-icon>
          </v-btn>
        </v-toolbar-items>
      </v-toolbar>
      <div class="info_layout pa-5">
        <div class="title_area">
          <div class="user__detail mb-10">
            <slot name="body" />
          </div>
        </div>
      </div>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters({
      title: "Mobile/diaglogTitle",
    }),
    diaglog: {
      get() {
        return this.$store.getters["Mobile/diaglogState"];
      },
      set(value) {
        this.$store.commit("Mobile/SET_DIAGLOG_STATE", value);
      },
    },
  },
  data() {
    return {};
  },
  methods: {
    close() {
      this.diaglog = false;
    },
  },
};
</script>
