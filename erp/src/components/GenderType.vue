<template>
  <v-icon
    class="mr-2"
    :color="gender || gender == 1 ? 'blue' : 'pink'"
    v-if="gender != null || gender != undefined"
  >
    {{ gender || gender == 1 ? "mdi-human-male" : "mdi-human-female" }}</v-icon
  >
</template>

<script>
export default {
  props: ["gender"],
};
</script>
