<template>
  <div>
    <img class="logo mb-1" src="/icons/check.png" />
    <h1 class="page_title text-center mb-5">{{ $t("register.completed") }}</h1>
    <label class="label_title">
      {{ $t("register.message2") }}<br />
      {{ $t("register.message3") }}
    </label>
    <v-btn
      type="submit"
      color="blue-grey"
      class="btn_login mb-5"
      style="margin-top:-10px; text-transform:none;"
      block
      @click="$router.push('/home')"
    >
      {{ $t("register.startusingapp") }}
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
