<template>
  <v-avatar size="36px">
    <img alt="Avatar" :src="user.avatar || 'icons/user.png'" />
  </v-avatar>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters({
      user: "Authen/getUser",
    }),
  },
};
</script>

<style></style>
