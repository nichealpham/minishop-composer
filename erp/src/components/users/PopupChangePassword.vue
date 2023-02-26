<template>
  <v-dialog v-model="showDialog" max-width="600px" eager>
    <v-card class="pa-0 ma-0">
      <v-card-title>
        {{ $t("common.change") }} {{ $t("user.password") }}
      </v-card-title>
      <v-form v-model="valid" @submit.prevent="save">
        <v-card-text class="pa-0 ma-0 pl-5 pr-5">
          <v-text-field
            v-model="body.oldPassword"
            :label="$t('common.oldPassword')"
            outlined
            required
            :rules="oldPasswordRules"
            :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
            :type="show1 ? 'text' : 'password'"
            @click:append="show1 = !show1"
          ></v-text-field>
          <v-text-field
            v-model="body.newPassword"
            :label="$t('common.newPassword')"
            outlined
            required
            :rules="newPasswordRules"
            :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
            :type="show2 ? 'text' : 'password'"
            @click:append="show2 = !show2"
          ></v-text-field>
          <v-text-field
            v-model="body.retypePassword"
            :label="$t('common.retypePassword')"
            outlined
            required
            :rules="retypePasswordRules"
            :append-icon="show3 ? 'mdi-eye' : 'mdi-eye-off'"
            :type="show3 ? 'text' : 'password'"
            @click:append="show3 = !show3"
          ></v-text-field>
        </v-card-text>
        <v-card-actions class="pb-5">
          <v-spacer></v-spacer>
          <v-btn color="darken-1" text @click="close" :disabled="loading">{{
            $t("common.cancel")
          }}</v-btn>
          <v-btn
            class="pl-10 pr-10 ml-10 white--text"
            color="#6166f5"
            :loading="loading"
            type="submit"
            :disabled="!valid"
            >{{ $t("common.confirmed") }}</v-btn
          >
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script>
import { sleep } from "@/plugins/helpers";
import { OAuthSignOut } from "@/plugins/firebase";

export default {
  data() {
    return {
      valid: false,
      loading: false,
      showDialog: false,
      show1: false,
      show2: false,
      show3: false,
      body: {
        oldPassword: "",
        newPassword: "",
        retypePassword: "",
      },
      oldPasswordRules: [
        (v) => !!v || "Old Password is required",
        (v) => v.length >= 6 || "Password must be bigger than 6 characters",
      ],
      newPasswordRules: [
        (v) => !!v || "New Password is required",
        (v) => v.length >= 6 || "Password must be bigger than 6 characters",
        (v) =>
          v != this.body.oldPassword ||
          "Password must new different from old password",
      ],
      retypePasswordRules: [
        (v) => !!v || "Re-type Password is required",
        (v) => v.length >= 6 || "Password must be bigger than 6 characters",
        (v) => v == this.body.newPassword || "Password retype mismatch",
      ],
    };
  },
  created() {},
  methods: {
    open() {
      this.showDialog = true;
    },
    async save() {
      this.loading = true;
      var result = await this.$httpClient.put(
        `/user/changepassword`,
        null,
        this.body
      );
      if (!result || result.error) {
        this.showErrorPopup(this.$t("common.changePasswordFailMessage"));
        this.loading = false;
        return;
      }
      var fcmToken = this.$store.getters["Authen/getFcm"];
      this.$httpClient.post("user/notifications/unsubscribe", null, null, {
        fcmToken,
      });
      await sleep(50);
      OAuthSignOut();
      window.parent.postMessage(JSON.stringify({ channel: "Logout" }), "*");
      this.$store.commit("Authen/LOGOUT");
      this.$store.commit("Signup/CLEAR_REGISTER");
      this.showSuccess(this.$t("common.changePasswordSuccessMessage"));
      this.close();
      await sleep(50);
      this.$router.push("/login");
    },
    clean() {
      this.loading = false;
      this.body = {
        oldPassword: "",
        newPassword: "",
        retypePassword: "",
      };
      this.show1 = false;
      this.show2 = false;
      this.show3 = false;
    },
    close() {
      this.clean();
      this.showDialog = false;
    },
  },
};
</script>
