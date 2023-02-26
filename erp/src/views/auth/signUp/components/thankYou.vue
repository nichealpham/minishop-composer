<template>
  <div>
    <img class="logo mb-1" src="/icons/check.png" />
    <h1 class="page_title text-center mb-5">{{ $t("register.completed") }}</h1>
    <label class="label_title">
      {{ $t("register.message2") }}
    </label>
    <v-btn
      type="submit"
      color="blue-grey"
      class="btn_login mb-5"
      style="margin-top: -10px; text-transform: none"
      block
      @click="$router.push('/home')"
    >
      {{ $t("register.startusingapp") }}
    </v-btn>

    <v-divider style="margin-top: 30px"></v-divider>
    <label class="label_or mb-5 white--text">{{ $t("auth.or") }}</label>
    <!-- <v-btn disabled color="blue-grey" block>
      {{ $t("register.becomedoctor") }}
    </v-btn> -->
    <v-btn color="blue-grey" block @click="$emit('continue')" class="mt-3">
      {{ $t("register.registerclinic") }}
    </v-btn>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },
  mounted() {},
  methods: {},
};
</script>
